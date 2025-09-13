namespace ReservaTurnosElkinRodriguez.DTOs
{
    public class TurnoResponseDto
    {
        public int IdTurno { get; set; }
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = string.Empty;
        public string NombreComercio { get; set; } = string.Empty;
        public string FechaTurno { get; set; } = string.Empty;
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFin { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
