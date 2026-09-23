using System.ComponentModel.DataAnnotations.Schema;

namespace ArriendosApi.Entities
{

    public class Inmueble
    {
        public int IdInmueble { get; set; }
        
        [ForeignKey("Edificio")]
        public int IdEdificio { get; set; }
        public string NumeroDepa { get; set; } = string.Empty;
        public string NumeroMedidorLuz { get; set; } = string.Empty;
        public string NumeroMedidorAgua { get; set; } = string.Empty;
        public string Estado { get; set; } = Estados.Inmueble.Disponible;

        public Edificio? Edificio { get; set; }
        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
