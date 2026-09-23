using ArriendosApi.Entities;

namespace ArriendosApi.DTOs
{
    public class CobroMensualReadDto
    {
        public int IdCobro { get; set; }
        public int IdContrato { get; set; }

        public string NombreInquilino { get; set; } = string.Empty;
        public string NumeroDepa { get; set; } = string.Empty;
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal ValorArriendo { get; set; }
        public decimal ValorAgua { get; set; }
        public decimal ValorLuz { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; } 
        public DateTime? FechaUltimoPago { get; set; }

    }
}

