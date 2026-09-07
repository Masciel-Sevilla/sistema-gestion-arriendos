using System.ComponentModel.DataAnnotations.Schema;

namespace ArriendosApi.Entities
{

    public class Contrato
    {
        
        public int IdContrato { get; set; }
        [ForeignKey("Inmueble")]
        public int IdInmueble { get; set; }
        [ForeignKey("Inquilino")]
        public int IdInquilino { get; set; }
        public decimal MontoArriendo { get; set; }
        public decimal MontoGarantia { get; set; }
        public int DiaPagoMensual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; } 
        public bool EsActivo { get; set; }

        public Inquilino? Inquilino { get; set; }
        public Inmueble? Inmueble { get; set; }
        public ICollection<CobroMensual> CobrosMensuales { get; set; }=new List<CobroMensual>();
    }
}
