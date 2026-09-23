using ArriendosApi.Context;
using ArriendosApi.DTOs;
using AutoMapper;
using ArriendosApi.Entities;
using Microsoft.EntityFrameworkCore;
using ArriendosApi.Exceptions;

namespace ArriendosApi.Services
{
    public class InmuebleService : IInmuebleService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;


        public InmuebleService(AppDBContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InmuebleReadDto>> getInmuebles(string Estado) {
            var query = _context.Inmuebles.Include(i=>i.Edificio).AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(Estado)) {
                if (!Estados.Inmueble.EsValido(Estado)) {
                    throw new EstadoInvalidoException(Estado);
                }

                query = query.Where(i => i.Estado == Estado);
            }
            var inmuebles = await query.ToListAsync();
            return _mapper.Map<IEnumerable<InmuebleReadDto>>(inmuebles);
        }

        public async Task<InmuebleReadDto> createInmubel(InmuebleCreateDto dto) {
            var edificioExiste = await _context.Edificios.AnyAsync(e => e.IdEdificio == dto.IdEdificio);
            if (!edificioExiste) { throw new ReferenciaInvalidaException($"No existe un edificio con id {dto.IdEdificio}."); }

            var inmueble = _mapper.Map<Inmueble>(dto);

            inmueble.Estado = Estados.Inmueble.Disponible;
            _context.Inmuebles.Add(inmueble);
            await _context.SaveChangesAsync();
            await _context.Entry(inmueble).Reference(i => i.Edificio).LoadAsync();
            return _mapper.Map<InmuebleReadDto>(inmueble);
        }
        public async Task<InmuebleReadDto> GetInmuebleById(int id) {
            var inmueble = await _context.Inmuebles.Include(i => i.Edificio).FirstOrDefaultAsync(i => i.IdInmueble == id);


            if (inmueble == null)
                throw new NoEncontradoException("Inmueble", id);

            return _mapper.Map<InmuebleReadDto>(inmueble);
        }

        public async Task UpdateInmueble(int id, InmuebleCreateDto dto) {
            var inmueble = await _context.Inmuebles.FindAsync(id);
            if (inmueble == null) { throw new NoEncontradoException("Inmueble", id); }
            if (inmueble.Estado == Estados.Inmueble.Inactivo)
                throw new OperacionNoPermitidaException("No se puede actualizar un inmueble inactivo.");


            if (inmueble.IdEdificio != dto.IdEdificio)
            {
                var edificioExiste = await _context.Edificios.AnyAsync(e => e.IdEdificio == dto.IdEdificio);
                if (!edificioExiste)
                {
                    throw new ReferenciaInvalidaException($"No existe un edificio con id {dto.IdEdificio}.");
                }
            }
            _mapper.Map(dto, inmueble);
            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoInmueble(int id, string Estado) {
            var inmueble = await _context.Inmuebles.FindAsync(id);
            if (inmueble == null)
            {
                throw new NoEncontradoException("Inmueble", id);
            }
            if (!Estados.Inmueble.EsValido(Estado))
            {
                throw new EstadoInvalidoException(Estado);

            }
            inmueble.Estado = Estado;
            await _context.SaveChangesAsync();
        }


    }
}
