using ArriendosApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace ArriendosApi.DTOs
{
    public class CobroMensualCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un contrato válido.")]
        public int IdContrato { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Debe seleccionar un mes válido.")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(2020, 2099, ErrorMessage = "Ingrese un año válido.")]
        public int Anio { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "El valor debe ser mayor que 0")]
        public decimal ValorArriendo { get; set; }

        [Required]
        [Range(0.00, 1000.00, ErrorMessage = "El valor no puede ser negativo.")]
        public decimal ValorAgua { get; set; }

        [Required]
        [Range(0.00, 1000.00, ErrorMessage = "El valor no puede ser negativo.")]
        public decimal ValorLuz { get; set; }

        [Required]
        [Range(0.00, 1000.00, ErrorMessage = "El valor no puede ser negativo.")]
        public decimal SaldoAnterior { get; set; }

        [Required]
        [Range(0, 1000.00, ErrorMessage = "El valor no puede ser negativo.")]
        public decimal MontoPagado { get; set; }

        [Required]
        public string Estado { get; set; } 
    }
}