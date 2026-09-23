namespace ArriendosApi.DTOs
{
    public class EdificioReadDto
    {
        public int IdEdificio { get; set; }
        public string Nombre { set; get; } = string.Empty;
        public string Direccion { set; get; } = string.Empty;
        public bool Estado { set; get; } 
    }
}
