using ArriendosApi.Context;
using ArriendosApi.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArriendosApi.Entities;
using ArriendosApi.Services;


namespace ArriendosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InmueblesController : ControllerBase
    {
        private readonly IInmuebleService _inmuebleService;

        public InmueblesController(IInmuebleService service)
        {
            _inmuebleService = service;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InmuebleReadDto>>> GetInmubles([FromQuery] string Estado = null)
        {
            var inmuebles = await _inmuebleService.getInmuebles(Estado);
            return Ok(inmuebles);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InmuebleReadDto>> GetInmuebleById(int id)
        {
            var inmueble = await _inmuebleService.GetInmuebleById(id);
            return Ok(inmueble);
        }

        [HttpPost]
        public async Task<ActionResult<InmuebleReadDto>> PostInmueble(InmuebleCreateDto dto)
        {
            var inmueble = await _inmuebleService.createInmubel(dto);
            return CreatedAtAction(nameof(GetInmuebleById), new { id = inmueble.IdInmueble }, inmueble);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInmueble(int id, InmuebleCreateDto dto)
        {
            await _inmuebleService.UpdateInmueble(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/cambiar-estado")]
        public async Task<IActionResult> CambiarEstado(int id,string estado)
        {
            await _inmuebleService.CambiarEstadoInmueble(id, estado);
            return NoContent();
        }
    }
}
