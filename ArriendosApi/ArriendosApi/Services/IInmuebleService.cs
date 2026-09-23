using ArriendosApi.DTOs;
using System.Threading.Tasks;
namespace ArriendosApi.Services
{
    public interface IInmuebleService
    {
        Task<IEnumerable<InmuebleReadDto>> getInmuebles(string Estado);
        Task<InmuebleReadDto> createInmubel(InmuebleCreateDto dto);

        Task<InmuebleReadDto> GetInmuebleById(int id);

        Task UpdateInmueble(int id, InmuebleCreateDto dto);
        Task CambiarEstadoInmueble(int id, string Estado);
        

        }
    }
