namespace ReservaTurnosElkinRodriguez.Models
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public int IdComercio { get; set; }
        public string NombreServicio { get; set; } = string.Empty;
        public TimeOnly HoraApertura { get; set; }
        public TimeOnly HoraCierre { get; set; }
        public int Duracion { get; set; } // Duracion en minutos
        public virtual Comercio Comercio { get; set; } = null!;
        public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
