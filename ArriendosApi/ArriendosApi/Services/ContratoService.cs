using ArriendosApi.Context;
using ArriendosApi.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ArriendosApi.Exceptions;
using ArriendosApi.Entities;

namespace ArriendosApi.Services
{
    public class ContratoService: IContratoService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ContratoService(AppDBContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ContratoReadDto>> getContratos(string estado) {
            var query =  _context.Contratos.Include(c => c.Inmueble).ThenInclude(i => i.Edificio).Include(c => c.Inquilino).AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(estado))
            {
                if (!Estados.Contrato.EsValido(estado)) {
                    throw new EstadoInvalidoException(estado); ;
                }
                query = query.Where(i => i.Estado == estado);
            }

            var contratos = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ContratoReadDto>>(contratos);
        }

        public async Task<ContratoReadDto> CreateContrato(ContratoCreateDto dto)
        {
            var inquilino = await _context.Inquilinos.AnyAsync(i=>i.IdInquilino==dto.IdInquilino);
            if (!inquilino) { throw new NoEncontradoException("Inquilino",dto.IdInquilino); }
            var inmueble = await _context.Inmuebles.FindAsync(dto.IdInmueble);
            if (inmueble == null) { throw new NoEncontradoException("Inmueble", dto.IdInmueble); }

            if (inmueble.Estado != Estados.Inmueble.Disponible) {
             
                throw new OperacionNoPermitidaException("EL inmueble no esta disponible");
            }
            var contrato=_mapper.Map<Contrato>(dto);
            contrato.Estado = Estados.Contrato.Vigente;
            _context.Contratos.Add(contrato);
            inmueble.Estado = Estados.Inmueble.Ocupado;

            await _context.SaveChangesAsync();
            await _context.Entry(contrato).Reference(c => c.Inquilino).LoadAsync();
            await _context.Entry(contrato).Reference(c => c.Inmueble).LoadAsync();

            if (contrato.Inmueble != null) {
                await _context.Entry(contrato.Inmueble).Reference(i => i.Edificio).LoadAsync();

            }

            return _mapper.Map<ContratoReadDto>(contrato);
        }

        public async Task<ContratoReadDto> getContratoById(int id) {
        var contrato= await _context.Contratos.Include(c=>c.Inmueble).ThenInclude(i=>i.Edificio).Include(c=>c.Inquilino).FirstOrDefaultAsync(i=>i.IdContrato==id);
            if (contrato == null) { throw new NoEncontradoException("Contratos", id); }
            return _mapper.Map<ContratoReadDto>(contrato);
        }

        public async Task UpdateContrato(int id, ContratoCreateDto dto)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            if (contrato == null)
            {
                throw new NoEncontradoException("Contrato", id);
            }

           
            // No se puede modificar un contrato finalizado
            if (contrato.Estado == Estados.Contrato.Finalizado|| contrato.Estado == Estados.Contrato.Cancelado)
            {
                throw new OperacionNoPermitidaException(
                    "No se puede actualizar un contrato finalizado o Cancelado."
                );
            }
            if (!Estados.Contrato.EsValido(dto.Estado))
            {
                throw new ReferenciaInvalidaException(
                    $"El estado '{dto.Estado}' no es un estado válido para un contrato."
                );
            }

            // Validar inmueble si está cambiando
            if (contrato.IdInmueble != dto.IdInmueble)
            {
                var inmueble = await _context.Inmuebles
                    .FirstOrDefaultAsync(i => i.IdInmueble == dto.IdInmueble);

                if (inmueble == null)
                {
                    throw new ReferenciaInvalidaException(
                        $"No existe un inmueble con id {dto.IdInmueble}."
                    );
                }

                if (inmueble.Estado != Estados.Inmueble.Disponible)
                {
                    throw new ReferenciaInvalidaException(
                        "El inmueble no está disponible."
                    );
                }

                // El inmueble anterior vuelve a estar disponible
                var inmuebleAnterior = await _context.Inmuebles
                    .FirstOrDefaultAsync(i => i.IdInmueble == contrato.IdInmueble);

                if (inmuebleAnterior != null)
                {
                    inmuebleAnterior.Estado = Estados.Inmueble.Disponible;
                }

                // El nuevo inmueble queda ocupado
                inmueble.Estado = Estados.Inmueble.Ocupado;
            }

            // Validar inquilino si está cambiando
            if (contrato.IdInquilino != dto.IdInquilino)
            {
                var inquilino = await _context.Inquilinos
                    .FirstOrDefaultAsync(i => i.IdInquilino == dto.IdInquilino);

                if (inquilino == null)
                {
                    throw new ReferenciaInvalidaException(
                        $"No existe un inquilino con id {dto.IdInquilino}."
                    );
                }

                if (inquilino.Estado == false)
                {
                    throw new ReferenciaInvalidaException(
                        $"El inquilino está desactivado con id {dto.IdInquilino}."
                    );
                }
            }

           
            _mapper.Map(dto, contrato);

            await _context.SaveChangesAsync();
        }

        public async Task CambairEstado(int id, string estado)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null) { throw new NoEncontradoException("Contrato", id); }
            if (!Estados.Contrato.EsValido(estado)) { throw new EstadoInvalidoException(estado); }
            contrato.Estado= estado;
            await _context.SaveChangesAsync();
        }
            
        }

    
}
