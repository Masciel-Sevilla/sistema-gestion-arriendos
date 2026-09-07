using ArriendosApi.Context;
using ArriendosApi.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArriendosApi.Entities;

namespace ArriendosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ContratosController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoReadDto>>> GetContratos(
            [FromQuery] bool ? esActivo,
            [FromQuery] int ? idInquilino,
            [FromQuery]int ? idInmueble
            )

        {

            var query = _context.Contratos.Include(c => c.Inquilino).Include(c => c.Inmueble).AsNoTracking().AsQueryable();

            if (esActivo.HasValue) {
                query = query.Where(c => c.EsActivo == esActivo.Value);
            }
            if (idInquilino.HasValue)
            {
                query = query.Where(c => c.IdInquilino == idInquilino.Value);
            }

            if (idInmueble.HasValue)
            {
                query = query.Where(c => c.IdInmueble == idInmueble.Value);
            }


            var Contratos = await query.ToListAsync();
                
            return Ok(_mapper.Map<IEnumerable<ContratoReadDto>>(Contratos));
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ContratoReadDto>> GetContratoById(int id)
        {

            var contrato = await _context.Contratos.Include(c => c.Inquilino)
                .Include(c => c.Inmueble).ThenInclude(i => i!.Edificio).FirstOrDefaultAsync(c => c.IdContrato == id);

            if (contrato == null)
            {
                return NotFound("no encontrado contrato");
            }
            return Ok(_mapper.Map<ContratoReadDto>(contrato));
        }

        [HttpPost]
        public async Task<ActionResult<ContratoReadDto>> PostContrato(ContratoCreateDto dto)
        {

            var inquilino = await _context.Inquilinos.AnyAsync(i => i.IdInquilino == dto.IdInquilino);
            if (!inquilino)
            {
                return BadRequest("El id del inquilno no existe");
            }
            var inmueble = await _context.Inmuebles.FindAsync(dto.IdInmueble);
            if (inmueble==null)
            {
                return BadRequest("El id del inmueble no existe");

            }
            if (inmueble.Estado)
            {
                return BadRequest("Eses inmueble esta ocupado");
            }


            var contrato = _mapper.Map<Contrato>(dto);
            contrato.EsActivo = true;

            _context.Contratos.Add(contrato);

            inmueble.Estado = true;
            await _context.SaveChangesAsync();

            await _context.Entry(contrato).Reference(c => c.Inquilino).LoadAsync();
            await _context.Entry(contrato).Reference(c => c.Inmueble).LoadAsync();
            if (contrato.Inmueble != null)
            {
                await _context.Entry(contrato.Inmueble).Reference(i => i.Edificio).LoadAsync();
            }

            var contratoRead = _mapper.Map<ContratoReadDto>(contrato);
            return CreatedAtAction(nameof(GetContratoById), new { id =contratoRead.IdContrato }, contratoRead);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutContrato(int id, ContratoCreateDto dto)
        {

            var contrato = await _context.Contratos.Include(c => c.Inmueble).FirstOrDefaultAsync(c => c.IdContrato == id);
            if (contrato == null)
            {
                return NotFound("Contrato no existe");
            }
            bool estadoAnterior = contrato.EsActivo;
            _mapper.Map(dto, contrato);

            if (contrato.Inmueble != null)
            {
                if (estadoAnterior && !contrato.EsActivo)
                {
                    contrato.Inmueble.Estado = false;
                }
                else if (!estadoAnterior && contrato.EsActivo)
                {
                    contrato.Inmueble.Estado = true;
                }

            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/cancelar")]

        public async Task<IActionResult> CancelarContrato(int id)
        {
            var contrato = await _context.Contratos.Include(c => c.Inmueble).FirstOrDefaultAsync(c => c.IdContrato == id);

            if (contrato == null) {
                return NotFound("no se encontro ese contrato");
            }
            if (!contrato.EsActivo) {
                return BadRequest("El contrato ya se encuentra inactivo");
            }
            contrato.EsActivo = false;
            contrato.FechaFin = DateTime.UtcNow;

            if (contrato.Inmueble != null) {
                contrato.Inmueble.Estado = false;
            }
            await _context.SaveChangesAsync();
            return Ok(new { mensaje=$"El contrato {id} ha sido inhabilitado y el inmuble {contrato.Inmueble.IdInmueble} liberado"});
        
        }


            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contrato = await _context.Contratos.Include(c => c.Inmueble).FirstOrDefaultAsync(i => i.IdContrato == id);

            if (contrato == null) { return NotFound("el contrato no existe"); }

            if (contrato.EsActivo && contrato.Inmueble != null)
            {
                contrato.Inmueble.Estado = false;
            }
            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();
            return NoContent();



        }
    }
}
