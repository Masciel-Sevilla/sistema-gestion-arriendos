namespace ArriendosApi.DTOs
{
    public class ContratoCreateDto
    {
        
        public int IdInmueble { get; set; }
        public int IdInquilino { get; set; }
        public decimal MontoArriendo { get; set; }
        public decimal MontoGarantia { get; set; }
        public int DiaPagoMensual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsActivo { get; set; }
    }
}
