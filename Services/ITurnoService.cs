using ReservaTurnosElkinRodriguez.DTOs;

namespace ReservaTurnosElkinRodriguez.Services
{
    public interface ITurnoService
    {
        Task<ApiResponseDto<List<TurnoResponseDto>>> GenerarTurnosAsync(GenerarTurnosRequestDto request);
        Task<ApiResponseDto<List<TurnoResponseDto>>> ConsultarTurnosDisponiblesAsync(int idServicio, string? fechaInicio = null, string? fechaFin = null);       
    }
}
