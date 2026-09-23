
using ArriendosApi.DTOs;
using ArriendosApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArriendosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquilinosController : ControllerBase
    {
        private readonly IInquilinoService _inquilinoService;
        public InquilinosController(IInquilinoService inquilinoService) {
            _inquilinoService = inquilinoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InquilinoReadDto>>> GetInquilinos([FromQuery] bool incluirInactivos) {
            var inquilinos = await _inquilinoService.getInquilinos(incluirInactivos);
            return Ok(inquilinos);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InquilinoReadDto>> GetInquilinoById(int id) {
            var res = await _inquilinoService.GetInquilinoById(id);
            if (res == null) {
                return NotFound("Inquilino no encontrado");
            }
            return Ok(res);
        }
        [HttpPost]

        public async Task<ActionResult<InquilinoReadDto>> PostInquilino(InquilinoCreateDto dto) {

            
            var inquilino = await _inquilinoService.CreateInquilino(dto);
            if (inquilino == null)
            {
                return BadRequest("La identificacion ya pertenece a un inquilino más");
            }

             return CreatedAtAction(nameof(GetInquilinoById), new { id = inquilino.IdInquilino }, inquilino);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutInquilino(InquilinoCreateDto inquilino, int id)
        {
            try {
                var actualizado = await _inquilinoService.UpdateInquilino(id,inquilino);
                if (!actualizado) return NotFound("El Inquilino no existe o la identificacion se repite.");
                return NoContent();
            }
            catch (InvalidOperationException ex) { 
                return BadRequest(ex.Message);
            }

        }
        
        [HttpPatch("{id}/cambiar-estado")]

        public async Task<ActionResult> CambiarEstadoInquilino(int id, bool nuevoEstado) {
            var cambio = await _inquilinoService.CambiarEstado(id,nuevoEstado);

            if (cambio == false)
            {
                return NotFound("Inquilino no encontrado");

            }

            return Ok(new { mensaje = $"El inquilino ahora está {(nuevoEstado ? "Activo" : "Inactivo")}" });
        }
        
        
    }
}
