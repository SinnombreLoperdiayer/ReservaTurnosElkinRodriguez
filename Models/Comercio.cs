namespace ReservaTurnosElkinRodriguez.Models
{
    public class Comercio
    {
        public int IdComercio { get; set; }
        public string NombreComercio { get; set; } = string.Empty;
        public int AforoMaximo { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    }
}
