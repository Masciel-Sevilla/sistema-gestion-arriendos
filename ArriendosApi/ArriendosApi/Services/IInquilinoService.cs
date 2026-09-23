using ArriendosApi.DTOs;

namespace ArriendosApi.Services
{
    public interface IInquilinoService
    {
        Task<IEnumerable<InquilinoReadDto>> getInquilinos(bool incluirInactivos);
        Task<InquilinoReadDto> GetInquilinoById(int id);
        Task<InquilinoReadDto> CreateInquilino(InquilinoCreateDto dto);

        Task<bool> UpdateInquilino(int id, InquilinoCreateDto dto);
        Task<bool> CambiarEstado(int id, bool nuevoEstado);
    }
}
