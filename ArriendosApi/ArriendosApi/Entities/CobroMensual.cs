using System.ComponentModel.DataAnnotations.Schema;

namespace ArriendosApi.Entities
{
    public class CobroMensual
    {

        public int IdCobro { get; set; }
        [ForeignKey("Contrato")]
        public int IdContrato { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal ValorArriendo { get; set; }
        public decimal ValorAgua { get; set; }
        public decimal ValorLuz { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public bool EsPagado { get; set; } = false;
        public DateTime? FechaUltimoPago { get; set; }

        public Contrato? Contrato { get; set; }
    }
}
