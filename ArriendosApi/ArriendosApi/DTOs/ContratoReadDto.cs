namespace ArriendosApi.DTOs
{
    public class ContratoReadDto
    {
        public int IdContrato { get; set; }
        public int IdInmueble { get; set; }
        public string NumeroDepa { get; set; } = string.Empty;
        public string NombreEdificio { get; set; } = string.Empty;

        public int IdInquilino { get; set; }
        public string NombreInquilino { get; set; } = string.Empty;
        public string IdentificacionInquilino { get; set; } = string.Empty;
        public decimal MontoArriendo { get; set; }
        public decimal MontoGarantia { get; set; }
        public int DiaPagoMensual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsActivo { get; set; }
    }
}
