using ArriendosApi.Context;
using ArriendosApi.DTOs;
using ArriendosApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArriendosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquilinosController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public InquilinosController(AppDBContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InquilinoReadDto>>> GetInquilinos() {
            var inquilino= await _context.Inquilinos.ToListAsync(); 
            return Ok(_mapper.Map<IEnumerable<InquilinoReadDto>>(inquilino));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InquilinoReadDto>> GetInquilinoById(int id) {
            var res = await _context.Inquilinos.FindAsync(id);
            if (res == null) {
                return NotFound("Inquilino no encontrado");
            }
            return Ok(_mapper.Map<InquilinoReadDto>(res));
        }
        [HttpPost]

        public async Task<ActionResult<InquilinoReadDto>> PostInquilino(InquilinoCreateDto dto) {

            bool existeCedula = await _context.Inquilinos.AnyAsync(i => i.Identificacion == dto.Identificacion);
            if (existeCedula) {
                return BadRequest("Ya existe un inquilino con esa identificación");
            }
            
            var inquilino = _mapper.Map<Inquilino>(dto);

            _context.Inquilinos.Add(inquilino);
            await _context.SaveChangesAsync();
            var inquilinoReas = _mapper.Map<InquilinoReadDto>(inquilino);
            return CreatedAtAction(nameof(GetInquilinoById), new { id = inquilinoReas.IdInquilino }, inquilinoReas);

        }
        [HttpPut("{id}")]

        public async Task<ActionResult> PutInquilino(InquilinoCreateDto inquilino, int id)
        {
            var inquilinoDb = await _context.Inquilinos.FindAsync(id);
            if (inquilinoDb == null) { return NotFound("ID no encontrado"); }

            if (inquilinoDb.Identificacion != inquilino.Identificacion) { 
            bool existeCedula = await _context.Inquilinos.AnyAsync(i=> i.Identificacion==inquilino.Identificacion);
                if (existeCedula) {
                    return BadRequest("La identificacion ya pertenece a un inquilino más");
                }
            }

            _mapper.Map(inquilino, inquilinoDb);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteInquilino(int id) {
            var inquilino = _context.Inquilinos.Find(id);
            if (inquilino == null) {
                return NotFound("Inquilino no encontrado");

            }
            _context.Inquilinos.Remove(inquilino);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool InquilinoExists(int id) {
            return _context.Inquilinos.Any(i=>i.IdInquilino==id);
        }


    }
}
