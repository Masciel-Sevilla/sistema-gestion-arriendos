namespace ArriendosApi.Entities
{

    public class Inquilino
    {
        public int IdInquilino { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty; 
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public bool Estado { get; set; } = true;

        public ICollection<Contrato> Contratos { get; set; }=new List<Contrato>();
    }
}
