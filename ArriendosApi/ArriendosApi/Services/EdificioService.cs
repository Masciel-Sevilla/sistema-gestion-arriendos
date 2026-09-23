using ArriendosApi.Context;
using ArriendosApi.DTOs;
using ArriendosApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArriendosApi.Services
{
    public class EdificioService:IEdificioService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public EdificioService(AppDBContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EdificioReadDto>> GetEdificios(bool incluirInactivos) {

            var query = _context.Edificios.AsNoTracking().AsQueryable();
            if (!incluirInactivos) {
                query = query.Where(e => e.Estado == true);
            }
            var edificios = await query.ToListAsync();
            return _mapper.Map<IEnumerable<EdificioReadDto>>(edificios);
        } 
        
        
        public async Task<EdificioReadDto> CreateEdificioAsync(EdificioCreateDto dto)
        {

            var edificio = _mapper.Map<Edificio>(dto);
            edificio.Estado = true;
            _context.Edificios.Add(edificio);
            await _context.SaveChangesAsync();

            return _mapper.Map<EdificioReadDto>(edificio);
        }

        public async Task<EdificioReadDto?> GetEdificiosByIdAsync(int id)
        {

            var edificio = await _context.Edificios.AsNoTracking().FirstOrDefaultAsync(e=>e.IdEdificio==id);
            
                return edificio==null?null: _mapper.Map<EdificioReadDto>(edificio);
        }

        public async Task<bool> UpdateEdificioAsync(int id, EdificioCreateDto dto)
        {

            var edificioDb = await _context.Edificios.FindAsync(id);
            
            if (edificioDb == null)
            {
                return false;
            }
            if (!edificioDb.Estado)
            {
                throw new InvalidOperationException("No se pueden modificar los datos de un edificio inactivo.");
            }
            _mapper.Map(dto, edificioDb);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CambiarEstadoAsync(int id, bool nuevoEstado)
        {
            var edificio = await _context.Edificios.FindAsync(id);
            if (edificio == null) { return false; }
            edificio.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return true;

        }

        }

}
