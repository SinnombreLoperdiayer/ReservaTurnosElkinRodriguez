using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservaTurnosElkinRodriguez.Data;
using ReservaTurnosElkinRodriguez.DTOs;
using System.Data;

namespace ReservaTurnosElkinRodriguez.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly ReservaTurnosContext _context;
        public TurnoRepository(ReservaTurnosContext context) 
        {
            _context = context;
        }
        public async Task<List<TurnoResponseDto>> ConsultarTurnosDisponiblesAsync(int idServicio, DateOnly? fechaInicio = null, DateOnly? fechaFin = null)
        {
            var result = new List<TurnoResponseDto>();
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IdServicio", SqlDbType.Int) { Value = idServicio }
            };

            if (fechaInicio.HasValue)
            {
                parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.Date) { Value = fechaInicio.Value.ToDateTime(TimeOnly.MinValue) });
            }
            else
            {
                parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.Date) { Value = DBNull.Value });
            }

            if (fechaFin.HasValue)
            {
                parameters.Add(new SqlParameter("@FechaFin", SqlDbType.Date) { Value = fechaFin.Value.ToDateTime(TimeOnly.MinValue) });
            }
            else
            {
                parameters.Add(new SqlParameter("@FechaFin", SqlDbType.Date) { Value = DBNull.Value });
            }

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "EXEC SP_ConsultarTurnosDisponibles @IdServicio, @FechaInicio, @FechaFin";
            command.Parameters.AddRange(parameters.ToArray());
           
            await _context.Database.OpenConnectionAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new TurnoResponseDto
                {
                    IdTurno = reader.GetInt32("id_turno"),
                    IdServicio = reader.GetInt32("id_servicio"),
                    NombreServicio = reader.GetString("nom_servicio"),
                    NombreComercio = reader.GetString("nom_comercio"),
                    FechaTurno = ((DateTime)reader["fecha_turno"]).ToString("dd/MM/yyyy"),
                    HoraInicio = ((TimeSpan)reader["hora_inicio"]).ToString(@"hh\:mm"),
                    HoraFin = ((TimeSpan)reader["hora_fin"]).ToString(@"hh\:mm"),
                    Estado = reader.GetString("estado")
                });
            }
            return result;
        }

        public async Task<List<TurnoResponseDto>> GenerarTurnosAsync(DateOnly fechaInicio, DateOnly fechaFin, int idServicio)
        {
            var result = new List<TurnoResponseDto>();

            var parameters = new[]
            {
                new SqlParameter("@FechaInicio", SqlDbType.Date) { Value = fechaInicio.ToDateTime(TimeOnly.MinValue) },
                new SqlParameter("@FechaFin", SqlDbType.Date) { Value = fechaFin.ToDateTime(TimeOnly.MinValue) },
                new SqlParameter("@IdServicio", SqlDbType.Int) { Value = idServicio }
            };

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "EXEC sp_GenerarTurnos @FechaInicio, @FechaFin, @IdServicio";
            command.Parameters.AddRange(parameters);

            await _context.Database.OpenConnectionAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new TurnoResponseDto
                {
                    IdTurno = reader.GetInt32("id_turno"),
                    IdServicio = reader.GetInt32("id_servicio"),
                    NombreServicio = reader.GetString("nom_servicio"),
                    NombreComercio = reader.GetString("nom_comercio"),
                    FechaTurno = ((DateTime)reader["fecha_turno"]).ToString("dd/MM/yyyy"),
                    HoraInicio = ((TimeSpan)reader["hora_inicio"]).ToString(@"hh\:mm"),
                    HoraFin = ((TimeSpan)reader["hora_fin"]).ToString(@"hh\:mm"),
                    Estado = reader.GetString("estado")
                });
            }

            return result;
        }
    }
}
