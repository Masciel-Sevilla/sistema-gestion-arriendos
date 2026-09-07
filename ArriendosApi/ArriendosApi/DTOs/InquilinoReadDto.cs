namespace ArriendosApi.DTOs
{
    public class InquilinoReadDto
    {
        public int IdInquilino { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
