namespace ReservaTurnosElkinRodriguez.Models
{
    public class Turno
    {
        public int IdTurno{ get; set; }
        public int IdServicio { get; set; }
        public DateOnly FechaTurno { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public EstadoTurno Estado { get; set; } = EstadoTurno.Disponible;
        public virtual Servicio Servicio { get; set; } = null!;
    }

    public enum EstadoTurno
    {
        Disponible,
        Reservado,
        Cancelado
    }
}
