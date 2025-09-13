using System.ComponentModel.DataAnnotations;

namespace ReservaTurnosElkinRodriguez.DTOs
{
    public class GenerarTurnosRequestDto
    {
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public string FechaInicio { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de fin es requerida")]
        public string FechaFin { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ID del servicio es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del servicio debe ser mayor a cero")]
        public int IdServicio { get; set; }
    }
}
