namespace ArriendosApi.DTOs
{
    public class InmuebleReadDto
    {
        public int IdInmueble { get; set; }
        public int IdEdificio { get; set; }
        public string NombreEdificio { set; get; } = string.Empty;
        public string NumeroDepa { get; set; } = string.Empty;
        public string NumeroMedidorLuz { get; set; } 
        public string NumeroMedidorAgua { get; set; } 
        public string Estado { get; set; }

    }
}
