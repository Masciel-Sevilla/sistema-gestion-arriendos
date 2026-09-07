using ArriendosApi.Context;
using ArriendosApi.DTOs;
using ArriendosApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ArriendosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CobrosMensualesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public CobrosMensualesController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobroMensualReadDto>>> GetCobrosMensuales(
            [FromQuery] int ? mes,
            [FromQuery] int ? anio,
            [FromQuery] bool ? esPagado,
            [FromQuery] int ? idContrato
            )
        {
            var query = _context.CobrosMensuales.Include(c => c.Contrato).ThenInclude(ct => ct!.Inquilino).
                Include(c => c.Contrato).ThenInclude(ct => ct!.Inmueble).AsNoTracking().AsQueryable();

            if (mes.HasValue) {
                query = query.Where(c => c.Mes == mes.Value);
            }
            if (anio.HasValue) {
                query = query.Where(c => c.Anio == anio.Value);
            }
            if (esPagado.HasValue) {
                query = query.Where(c => c.EsPagado == esPagado.Value);
            }
            if (idContrato.HasValue)
            {
                query = query.Where(c => c.IdContrato == idContrato.Value);
            }

            var cobros = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CobroMensualReadDto>>(cobros));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CobroMensualReadDto>> GetCobroMensual(int id)
        {
            var cobro = await _context.CobrosMensuales.Include(c => c.Contrato).ThenInclude(i => i!.Inquilino).
                    Include(c => c.Contrato).ThenInclude(i => i!.Inmueble).FirstOrDefaultAsync(c => c.IdContrato == id);

            if (cobro == null)
            {
                return NotFound("Cobro con ese ID no encontrado");
            }
            return Ok(_mapper.Map<CobroMensualReadDto>(cobro));
        }

        [HttpPost]

        public async Task<ActionResult<CobroMensualReadDto>> PostCobroMensual(CobroMensualCreateDto dto)
        {
            var contrato = await _context.Contratos.FindAsync(dto.IdContrato);
            if (contrato == null) { return BadRequest("El contrato no existe"); }

            bool existeCobro = await _context.CobrosMensuales
    .AnyAsync(c => c.IdContrato == dto.IdContrato && c.Mes == dto.Mes && c.Anio == dto.Anio);
            if (existeCobro)
            {
                return BadRequest($"Este cobro ya fue creado {dto.Mes} - {dto.Anio}");
            }
            var cobro = _mapper.Map<CobroMensual>(dto);
            CalcularSaldosYEstados(cobro);

            _context.CobrosMensuales.Add(cobro);
            await _context.SaveChangesAsync();
            await _context.Entry(cobro).Reference(c => c.Contrato).LoadAsync();
            if (cobro.Contrato != null)
            {
                await _context.Entry(cobro.Contrato).Reference(c => c.Inmueble).LoadAsync();
                await _context.Entry(cobro.Contrato).Reference(c => c.Inquilino).LoadAsync();
            }
            var cobroRead = _mapper.Map<CobroMensualReadDto>(cobro);
            return CreatedAtAction(nameof(GetCobroMensual), new { id = cobroRead.IdCobro }, cobroRead);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCobroMensual(int id, CobroMensualCreateDto dto)
        {
            var cobro = await _context.CobrosMensuales.FindAsync(id);
            if (cobro == null) { return NotFound("El cobro mensual no existe"); }

            _mapper.Map(dto, cobro);

            CalcularSaldosYEstados(cobro);

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}/registrar-pago")]
        public async Task<ActionResult<CobroMensualReadDto>> RegistrarPago(int id, [FromBody] CobroMensualPagoDto dto) {
            if (dto.MontoAbono <= 0) {
                return BadRequest("El monto del abono debe ser mayor a cero.");
            }
            var cobro = await _context.CobrosMensuales
        .Include(c => c.Contrato)
            .ThenInclude(ct => ct!.Inquilino)
        .Include(c => c.Contrato)
            .ThenInclude(ct => ct!.Inmueble)
        .FirstOrDefaultAsync(c => c.IdCobro == id);
            if (cobro == null)
            {
                return NotFound("No enocntrado");
            }
            if (cobro.EsPagado) {
                return BadRequest("Este cobro ya se encuentra pagado en su totalidad.");
            }
            if (dto.MontoAbono > cobro.SaldoPendiente) {
                return BadRequest($"el abono ({dto.MontoAbono}) no puede ser mayor al saldo pendienye {cobro.SaldoPendiente}");
            }

            cobro.MontoPagado += dto.MontoAbono;
            cobro.SaldoPendiente = cobro.TotalPagar - cobro.MontoPagado;
            cobro.FechaUltimoPago = DateTime.UtcNow;

            if (cobro.SaldoPendiente <= 0)
            {
                cobro.SaldoPendiente = 0;
                cobro.EsPagado = true;
            }        

            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<CobroMensualReadDto>(cobro));
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCobroMensual(int id)
        {
            var cobro = await _context.CobrosMensuales.FindAsync(id);
            if (cobro == null) return NotFound("El cobro mensual no existe.");

            _context.CobrosMensuales.Remove(cobro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private void CalcularSaldosYEstados(CobroMensual cobro)
        {
            cobro.TotalPagar = cobro.ValorArriendo + cobro.ValorAgua + cobro.ValorLuz + cobro.SaldoAnterior;
            cobro.SaldoPendiente = cobro.TotalPagar - cobro.MontoPagado;
            if (cobro.SaldoPendiente <= 0)
            {
                cobro.SaldoPendiente = 0;
                cobro.EsPagado = true;
            }
            else
            {
                cobro.EsPagado = false;
            }
            // Si se registró un pago, actualizamos la fecha del último pago si viene vacía
            if (cobro.MontoPagado > 0 && !cobro.FechaUltimoPago.HasValue)
            {
                cobro.FechaUltimoPago = DateTime.UtcNow;
            }

        }
    }
    }
