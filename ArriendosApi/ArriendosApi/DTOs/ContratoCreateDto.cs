using System.ComponentModel.DataAnnotations;

namespace ArriendosApi.DTOs
{
    public class ContratoCreateDto
    {

        [Required]
        public int IdInmueble { get; set; }
        [Required]
        public int IdInquilino { get; set; }
        [Required]
        public decimal MontoArriendo { get; set; }
        [Required]
        public decimal MontoGarantia { get; set; }
        [Required]
        public int DiaPagoMensual { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime? FechaFin { get; set; }

        public string Estado { get; set; }

    }
}
