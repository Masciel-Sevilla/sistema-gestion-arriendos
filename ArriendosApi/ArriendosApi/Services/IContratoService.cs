using ArriendosApi.DTOs;

namespace ArriendosApi.Services
{
    public interface IContratoService
    {
        Task<IEnumerable<ContratoReadDto>> getContratos(string estado);
        Task<ContratoReadDto> CreateContrato(ContratoCreateDto dto);
        Task<ContratoReadDto> getContratoById(int id);
        Task UpdateContrato(int id, ContratoCreateDto dto);
        Task CambairEstado(int id, string estado);
    }
}
