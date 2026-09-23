using ArriendosApi.Context;
using ArriendosApi.DTOs;
using AutoMapper;
using ArriendosApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArriendosApi.Services
{
    public class InquilinoService:IInquilinoService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public InquilinoService(AppDBContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InquilinoReadDto>> getInquilinos( bool incluirInactivos) {
            var query = _context.Inquilinos.AsNoTracking().AsQueryable();
            if (!incluirInactivos) { query = query.Where(i => i.Estado == true); }
            var inquilinos = await query.ToListAsync();
            return _mapper.Map<IEnumerable<InquilinoReadDto>>(inquilinos);
        
        }

        public async Task<InquilinoReadDto> GetInquilinoById(int id) {
            var inquilino = await _context.Inquilinos.FirstOrDefaultAsync(i=>i.IdInquilino==id);
            return inquilino == null ? null : _mapper.Map<InquilinoReadDto>(inquilino);
        }

        public async Task<InquilinoReadDto> CreateInquilino(InquilinoCreateDto dto) {
            var existeCedula =await _context.Inquilinos.AnyAsync(i => i.Identificacion == dto.Identificacion);
            if (existeCedula) { return null; }
            var inquilino = _mapper.Map<Inquilino>(dto);
            inquilino.Estado = true;
            _context.Inquilinos.Add(inquilino);
            await _context.SaveChangesAsync();

            return _mapper.Map<InquilinoReadDto>(inquilino);
        }

        public async Task<bool> UpdateInquilino(int id, InquilinoCreateDto dto)
        {
            var existeCedula = await _context.Inquilinos
                .AnyAsync(i => i.Identificacion == dto.Identificacion
                            && i.IdInquilino != id);

            if (existeCedula)
            {
                return false;
            }

            var inquilino = await _context.Inquilinos.FindAsync(id);

            if (inquilino == null)
            {
                return false;
            }

            if (!inquilino.Estado)
            {
                throw new InvalidOperationException(
                    "No se pueden modificar los datos de un inquilino inactivo.");
            }

            _mapper.Map(dto, inquilino);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CambiarEstado(int id,bool nuevoEstado) { 
            var inquilino=await _context.Inquilinos.FindAsync(id);
            if (inquilino == null) { return false; }
            inquilino.Estado= nuevoEstado;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
