using System.ComponentModel.DataAnnotations;

namespace ArriendosApi.DTOs
{
    public class EdificioCreateDto
    {
        [Required(ErrorMessage ="El {0} es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; } = string.Empty;
    }
}
