
using ArriendosApi.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ArriendosApi.Services;

namespace ArriendosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly IContratoService _contratoService;

        public ContratosController(IContratoService contratoServicecontext)
        {
            _contratoService = contratoServicecontext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ContratoReadDto>>> getContratos([FromQuery] string Estado = null)
        {
            var contratos = await _contratoService.getContratos(Estado);
            return Ok(contratos);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ContratoReadDto>> GetContratoById(int id)
        {

            var contrato = await _contratoService.getContratoById(id);
            return Ok(contrato);
        }

        [HttpPost]
        public async Task<ActionResult<ContratoReadDto>> PostContrato(ContratoCreateDto dto)
        {

            var contrato = await _contratoService.CreateContrato(dto);
            return CreatedAtAction(nameof(GetContratoById), new { id = contrato.IdContrato }, contrato);
        }


        [HttpPut]
        public async Task<IActionResult> PutContrato(int id, ContratoCreateDto dto)
        {
            await _contratoService.UpdateContrato(id, dto); 
            return NoContent();
        }
        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> CambiarEstado(int id, string estado)
        {
            await _contratoService.CambairEstado(id, estado);
            return NoContent();
        }

        /*
        public async Task<ActionResult<IEnumerable<ContratoReadDto>>> GetContratos(
            [FromQuery] string ? estado,
            [FromQuery] int ? idInquilino,
            [FromQuery]int ? idInmueble
            )

        {

            var query = _context.Contratos.Include(c => c.Inquilino).Include(c => c.Inmueble).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado)) {
                query = query.Where(c => c.Estado == estado);
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
        

    /*
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
}*/
    }
}
