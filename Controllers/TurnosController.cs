using Microsoft.AspNetCore.Mvc;
using ReservaTurnosElkinRodriguez.DTOs;
using ReservaTurnosElkinRodriguez.Services;
using System.ComponentModel.DataAnnotations;

namespace ReservaTurnosElkinRodriguez.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnoService _turnoService;
        private readonly ILogger<TurnosController> _logger;
        public TurnosController(ITurnoService turnoService, ILogger<TurnosController> logger)
        {
            _turnoService = turnoService;
            _logger = logger;
        }

        /// <summary>
        /// Genera turnos para un servicio específico en un rango de fechas.
        /// </summary>
        /// <param name="requestDto">Datos necesarios para generar los turnos.</param>"
        /// <returns>Lista de turnos generados o errores de validación.</returns>
        [HttpPost("generar")]
        [ProducesResponseType(typeof(ApiResponseDto<List<TurnoResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<List<TurnoResponseDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<List<TurnoResponseDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDto<List<TurnoResponseDto>>>> GenerarTurnos([FromBody] GenerarTurnosRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation("Iniciando generación de turnos para el servicio {IdServicio} desde {FechaInicio} hasta {FechaFin}",
                    requestDto.IdServicio, requestDto.FechaInicio, requestDto.FechaFin);

                if (!ModelState.IsValid)
                {
                    var response = new ApiResponseDto<List<TurnoResponseDto>>
                    {
                        Success = false,
                        Message = "Error en la validación de datos",
                        Errors = ModelState.Values
                             .SelectMany(v => v.Errors)
                             .Select(e => e.ErrorMessage)
                             .ToList()
                    };
                    return BadRequest(response);
                }

                var result = await _turnoService.GenerarTurnosAsync(requestDto);

                if (!result.Success)
                {
                    return BadRequest(result);

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al generar turnos para el servicio {IdServicio}", requestDto.IdServicio);
                var errorResponse = new ApiResponseDto<List<TurnoResponseDto>>
                {
                    Success = false,
                    Message = "Error inesperado al procesar la solicitud.",
                    Errors = new List<string> { ex.Message }
                };
                return StatusCode(500, errorResponse);
            }
        }

        /// <summary>
        /// Consulta turnos disponibles para un servicio específico
        /// </summary>
        /// <param name="idServicio">ID del servicio</param>
        /// <param name="fechaInicio">Fecha de inicio (opcional) en formato dd/MM/yyyy</param>
        /// <param name="fechaFin">Fecha fin (opcional) en formato dd/MM/yyyy</param>
        /// <returns>Lista de turnos disponibles</returns>
        [HttpGet("disponibles/{idServicio}")]
        [ProducesResponseType(typeof(ApiResponseDto<List<TurnoResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<List<TurnoResponseDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<List<TurnoResponseDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDto<List<TurnoResponseDto>>>> ConsultarTurnosDisponibles(
            [FromRoute, Required] int idServicio, 
            [FromQuery] string? fechaInicio = null, 
            [FromQuery] string? fechaFin = null)
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de turnos disponibles para el servicio {IdServicio} desde {FechaInicio} hasta {FechaFin}",
                    idServicio, fechaInicio, fechaFin);
                
                if (idServicio <= 0)
                {
                    var response = new ApiResponseDto<List<TurnoResponseDto>>
                    {
                        Success = false,
                        Message = "Error en la validación de datos",
                        Errors = new List<string> { "El ID del servicio debe ser mayor a cero." }
                    };
                    return BadRequest(response);
                }
                
                var result = await _turnoService.ConsultarTurnosDisponiblesAsync(idServicio, fechaInicio, fechaFin);
                
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al consultar turnos disponibles para el servicio {IdServicio}", idServicio);
                var errorResponse = new ApiResponseDto<List<TurnoResponseDto>>
                {
                    Success = false,
                    Message = "Error inesperado al procesar la solicitud.",
                    Errors = new List<string> { ex.Message }
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}
