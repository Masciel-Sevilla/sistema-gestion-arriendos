using ArriendosApi.DTOs;
using System.Threading.Tasks;

namespace ArriendosApi.Services
{
    public interface IEdificioService
    {
        Task<EdificioReadDto> CreateEdificioAsync(EdificioCreateDto dto);
        Task<EdificioReadDto?> GetEdificiosByIdAsync(int id);
        Task<bool> UpdateEdificioAsync(int id, EdificioCreateDto dto);
        Task<bool> CambiarEstadoAsync(int id, bool nuevoEstado);
        Task<IEnumerable<EdificioReadDto>> GetEdificios(bool incluirInactivos);
    }
}
