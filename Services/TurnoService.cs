using ReservaTurnosElkinRodriguez.DTOs;
using ReservaTurnosElkinRodriguez.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ReservaTurnosElkinRodriguez.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly ILogger<TurnoService> _logger;

        public TurnoService(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }
        public async Task<ApiResponseDto<List<TurnoResponseDto>>> ConsultarTurnosDisponiblesAsync(int idServicio, string? fechaInicio = null, string? fechaFin = null)
        {
            var response = new ApiResponseDto<List<TurnoResponseDto>>();

            try
            {
                DateOnly? fechaInicioDate = null;
                DateOnly? fechaFinDate = null;

                if(!string.IsNullOrEmpty(fechaInicio) || !string.IsNullOrEmpty(fechaFin))
                {
                    var validacionFecha = ValidarFechas(fechaInicio ?? "", fechaFin ?? "");
                    if (!validacionFecha.IsValid)
                    {
                        response.Success = false;
                        response.Message = "Error en la validación de fechas";
                        response.Errors = validacionFecha.Errors;
                        return response;
                    }
                    
                    fechaInicioDate = validacionFecha.FechaInicio;
                    fechaFinDate = validacionFecha.FechaFin;
                }

                var turnos = await _turnoRepository.ConsultarTurnosDisponiblesAsync(idServicio, fechaInicioDate, fechaFinDate);

                response.Success = true;
                response.Message = $"Se consultaron {turnos.Count} turos disponibles.";
                response.Data = turnos;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar turnos disponibles para servicio {IdServicio}", idServicio);
                response.Success = false;
                response.Message = "Error al consultar los turnos disponibles.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseDto<List<TurnoResponseDto>>> GenerarTurnosAsync(GenerarTurnosRequestDto request)
        {
            var response = new ApiResponseDto<List<TurnoResponseDto>>();

            try
            {
                var validacionFecha = ValidarFechas(request.FechaInicio, request.FechaFin);
                if (!validacionFecha.IsValid)
                {
                    response.Success = false;
                    response.Message = "Error en la validación de fechas";
                    response.Errors = validacionFecha.Errors;
                    return response;
                }

                var fechaInicio = validacionFecha.FechaInicio!.Value;
                var fechaFin = validacionFecha.FechaFin!.Value;

                var validacionNegocio = ValidarReglasNegocio(fechaInicio, fechaFin, request.IdServicio);
                if (!validacionNegocio.IsValid)
                {
                    response.Success = false;
                    response.Message = "Error en la validación de reglas de negocio";
                    response.Errors = validacionNegocio.Errors;
                    return response;
                }

                var turnosGenerados = await _turnoRepository.GenerarTurnosAsync(fechaInicio, fechaFin, request.IdServicio);

                response.Success = true;
                response.Message = $"Se generaron {turnosGenerados.Count} turnos.";
                response.Data = turnosGenerados;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar turnos para servicio {IdServicio}", request.IdServicio);
                response.Success = false;
                response.Message = "Error al generar los turnos.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        private ValidationResult ValidarFechas(string fechaInicio, string fechaFin)
        {
            var result = new ValidationResult();
            const string formatoEsperado = "dd/MM/yyyy";

            if (string.IsNullOrEmpty(fechaInicio))
            {
                result.Errors.Add("La fecha de inicio es requerida.");
            }
            else if (!DateTime.TryParseExact(fechaInicio, formatoEsperado, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaInicioDateTime))
            {
                result.Errors.Add($"El formato de la fecha de inicio es inválido. Use el formato '{formatoEsperado}'.");
            }
            else
            {
                result.FechaInicio = DateOnly.FromDateTime(fechaInicioDateTime);
            }

            if (string.IsNullOrEmpty(fechaFin))
            {
                result.Errors.Add("La fecha de fin es requerida.");
            }
            else if(!DateTime.TryParseExact(fechaFin, formatoEsperado, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaFinDateTime))
            {
                result.Errors.Add($"El formato de la fecha de fin es inválido. Use el formato '{formatoEsperado}'.");
            }
            else
            {
                result.FechaFin = DateOnly.FromDateTime(fechaFinDateTime);
            }

            if (result.FechaInicio.HasValue && result.FechaFin.HasValue)
            {
                if (result.FechaInicio.Value > result.FechaFin.Value)
                {
                    result.Errors.Add("La fecha de inicio no puede ser posterior a la fecha de fin.");
                }
            }

            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        private ValidationResult ValidarReglasNegocio(DateOnly fechaInicio, DateOnly fechaFin, int idServicio)
        {
            var result = new ValidationResult();

            var fechaActual = DateOnly.FromDateTime(DateTime.Now);
            if(fechaInicio < fechaActual)
            {
                result.Errors.Add("La fecha de inicio no puede ser anterior a la fecha actual.");
            }

            var diferenciaDias = (fechaFin.ToDateTime(TimeOnly.MinValue) - fechaInicio.ToDateTime(TimeOnly.MinValue)).TotalDays;
            if(diferenciaDias > 30)
            {
                result.Errors.Add("El rango entre la fecha de inicio y la fecha de fin no puede ser mayor a 30 días.");
            }

            if(idServicio <= 0)
            {
                result.Errors.Add("El ID del servicio debe ser mayor a cero.");
            }

            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        private class ValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; } = new();
            public DateOnly? FechaInicio { get; set; }
            public DateOnly? FechaFin { get; set; }
        }
    }
}
