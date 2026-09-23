
using ArriendosApi.DTOs;

using ArriendosApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System;

namespace ArriendosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdificiosController : ControllerBase
    {
        private readonly IEdificioService _edificioService;

        public EdificiosController(IEdificioService service)
        {
            _edificioService = service;
        }

        // GET: api/Edificios (Obtener todos los edificios)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EdificioReadDto>>> GetEdificios([FromQuery] bool incluirInactivos=false)
        {
            var edificios = await _edificioService.GetEdificios(incluirInactivos);
            return Ok(edificios);
        }
        [HttpPost]
        public async Task<ActionResult<EdificioReadDto>> PostEdificio(EdificioCreateDto dto) {

            var nuevoEdificio =await _edificioService.CreateEdificioAsync(dto);
            return CreatedAtAction(nameof(GetEdificiosById), new { id = nuevoEdificio.IdEdificio }, nuevoEdificio);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EdificioReadDto>> GetEdificiosById(int id) {

            var edificio = await _edificioService.GetEdificiosByIdAsync(id);
            if (edificio == null) return NotFound("El edificio no existe.");
            return Ok(edificio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEdificio(int id, EdificioCreateDto dto) {
            try
            {
                var actualizado = await _edificioService.UpdateEdificioAsync(id, dto);
                if (!actualizado) return NotFound("El edificio no existe.");
                return NoContent();
            }
            catch (InvalidOperationException ex) {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPatch("{id}/cambiar-estado")]

        public async Task<IActionResult> CambiarEstado(int id, [FromBody] bool nuevoEstado) {
            var cambio = await _edificioService.CambiarEstadoAsync(id, nuevoEstado);
            if (!cambio) return NotFound("El edificio no existe.");
            return Ok(new { mensaje = $"El edificio ahora está {(nuevoEstado ? "Activo" : "Inactivo")}" });
        }

       
    }
}
