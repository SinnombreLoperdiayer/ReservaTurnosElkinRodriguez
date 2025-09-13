using ReservaTurnosElkinRodriguez.DTOs;

namespace ReservaTurnosElkinRodriguez.Repositories
{
    public interface ITurnoRepository
    {
        Task<List<TurnoResponseDto>> GenerarTurnosAsync(DateOnly fechaInicio, DateOnly fechaFin, int idServicio);
        Task<List<TurnoResponseDto>> ConsultarTurnosDisponiblesAsync(int idServicio, DateOnly? fechaInicio = null, DateOnly? fechaFin = null );
    }
}
