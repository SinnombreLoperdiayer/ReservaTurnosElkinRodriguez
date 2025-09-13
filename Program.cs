
using Microsoft.EntityFrameworkCore;
using ReservaTurnosElkinRodriguez.Data;
using ReservaTurnosElkinRodriguez.Repositories;
using ReservaTurnosElkinRodriguez.Services;
using System.Text.Json.Serialization;

namespace ReservaTurnosElkinRodriguez
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddDbContext<ReservaTurnosContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ReservaTurnosConnection")));
          
            builder.Services.AddScoped<ITurnoService, TurnoService>();
            builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "ReservaTurnosElkinRodriguez API",
                    Version = "v1",
                    Description = "API para la gestión de reservas de turnos en comercios.",
                    Contact = new()
                    {
                        Name = "Elkin Rodriguez erodriguez@asesoftware.com",
                        Email = "erodriguez@asesoftware.com"
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
