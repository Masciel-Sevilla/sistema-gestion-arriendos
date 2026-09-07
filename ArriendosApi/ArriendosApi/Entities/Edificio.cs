namespace ArriendosApi.Entities
{
    public class Edificio
    {
        public int IdEdificio {  get; set; }
        public string Nombre { set; get; } = string.Empty;
        public string Direccion {  set; get; } = string.Empty;

        public ICollection<Inmueble> Inmuebles { set; get; } = new List<Inmueble>();
    
    }
}

