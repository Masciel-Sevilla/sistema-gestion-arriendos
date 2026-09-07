using ArriendosApi.Context;
using ArriendosApi.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArriendosApi.Entities;


namespace ArriendosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InmueblesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public InmueblesController(AppDBContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InmuebleReadDto>>> GetInmubles() {
            var inmuebles = await _context.Inmuebles.Include(i => i.Edificio).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<InmuebleReadDto>>(inmuebles));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InmuebleReadDto>> GetInmuebleById(int id) {
            var inmueble = await _context.Inmuebles.Include(i => i.Edificio).FirstOrDefaultAsync(i => i.IdInmueble == id);
            if (inmueble == null)
            {
                return NotFound("Inmueble no encontrado");
            }
            return Ok(_mapper.Map<InmuebleReadDto>(inmueble));
        }

        [HttpPost]
        public async Task<ActionResult<InmuebleReadDto>> PostInmueble(InmuebleCreateDto dto) {
            var edificioExiste = await _context.Edificios.AnyAsync(e => e.IdEdificio == dto.IdEdificio);
            if (!edificioExiste) {
                return BadRequest("No existe el edificio al que desea asignar");
            }
            var inmueble = _mapper.Map<Inmueble>(dto);
            _context.Inmuebles.Add(inmueble);
            await _context.SaveChangesAsync();

            await _context.Entry(inmueble).Reference(i => i.Edificio).LoadAsync();
            var inmuebleRead = _mapper.Map<InmuebleReadDto>(inmueble);

            return CreatedAtAction(nameof(GetInmuebleById), new { id = inmuebleRead.IdInmueble }, inmuebleRead);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutInmueble(int id, InmuebleCreateDto dto) {
            var inmueble = await _context.Inmuebles.FindAsync(id);
            if (inmueble == null)
            {
                return NotFound("El inmueble no existe");

            }

            if (inmueble.IdEdificio != dto.IdEdificio)
            {
                var edificio = await _context.Edificios.AnyAsync(i => i.IdEdificio == dto.IdEdificio);
                if (!edificio)
                {
                    return BadRequest("Edificio no encontrado");
                }

            }
            _mapper.Map(dto, inmueble);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInmueble(int id) {

            var inmueble = await _context.Inmuebles.FindAsync(id);
            if (inmueble == null) {
                return NotFound("No encontrado el inmueble");
            }
            _context.Inmuebles.Remove(inmueble);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
