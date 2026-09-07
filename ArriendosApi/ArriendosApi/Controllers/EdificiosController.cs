using ArriendosApi.Context;
using ArriendosApi.DTOs;
using ArriendosApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ArriendosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdificiosController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public EdificiosController(AppDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Edificios (Obtener todos los edificios)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EdificioReadDto>>> GetEdificios()
        {   var edificios= await _context.Edificios.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EdificioReadDto>>(edificios));
        }
        [HttpPost]
        public async Task<ActionResult<EdificioReadDto>> PostEdificio(EdificioCreateDto dto) {

            var edificio = _mapper.Map<Edificio>(dto);
            _context.Edificios.Add(edificio);
            await _context.SaveChangesAsync();

            var edificioRead = _mapper.Map<EdificioReadDto>(edificio);
            return CreatedAtAction(nameof(GetEdificios), new { id = edificioRead.IdEdificio }, edificioRead);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EdificioReadDto>> GetEdificiosById(int id) {
            
            var edificio= await _context.Edificios.FindAsync(id);
            if (edificio == null) { 
                return NotFound("EL EDIFICIO CON ESE ID NO FUE ENCONTRADO");
            }
            return Ok(_mapper.Map<EdificioReadDto>(edificio));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEdificio(int id, EdificioCreateDto dto) {

            var edificioDb = await _context.Edificios.FindAsync(id);
            if (edificioDb == null)
            {
                return NotFound("El edificio no existe.");
            }

            // Actualiza las propiedades de la entidad DB con los datos del DTO
            _mapper.Map(dto, edificioDb);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEdificio(int id) { 
        
            var edificio= await _context.Edificios.FindAsync(id);
            if (edificio == null)
            {
                return NotFound("EDIFICIO NO ENCONTRADO");
            }
            _context.Edificios.Remove(edificio);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EdificioExists(int id) {
            return _context.Edificios.Any(e => e.IdEdificio == id);
        }



    }
}
