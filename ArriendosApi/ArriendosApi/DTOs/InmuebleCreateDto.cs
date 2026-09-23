using System.ComponentModel.DataAnnotations;

namespace ArriendosApi.DTOs
{
    public class InmuebleCreateDto
    {
        [Required(ErrorMessage = "El edificio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un edificio válido.")]
        public int IdEdificio { get; set; }

        [Required(ErrorMessage = "El número de departamento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de departamento no puede exceder los 20 caracteres.")]
        public string NumeroDepa { get; set; } = string.Empty;

        [StringLength(12, ErrorMessage = "El medidor de luz no puede exceder los 12 caracteres.")]
        public string NumeroMedidorLuz { get; set; }
        [StringLength(5, ErrorMessage = "El medidor de luz no puede exceder los 5 caracteres.")]
        public string NumeroMedidorAgua { get; set; }
        

    }
}
