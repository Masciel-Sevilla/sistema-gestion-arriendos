using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ArriendosApi.DTOs
{
    public class InquilinoCreateDto
    {
        [Required(ErrorMessage ="Este campo {0} es obligatorio")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="El campo debe tener entre {1} y {2} caracteres")]
        public string Nombres { get; set; } = string.Empty;


        [Required(ErrorMessage = "La identificación es obligatoria.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula o identificación debe tener exactamente 10 dígitos.")]
        [RegularExpression("^[0-9]+$",ErrorMessage ="La identificación solo debe tener números")]
        public string Identificacion { get; set; } = string.Empty;
        [Required(ErrorMessage ="El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre {2} y {1} caracteres.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string? Telefono { get; set; }

       

    }
}
