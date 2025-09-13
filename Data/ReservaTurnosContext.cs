using Microsoft.EntityFrameworkCore;
using ReservaTurnosElkinRodriguez.Models;

namespace ReservaTurnosElkinRodriguez.Data
{
    public class ReservaTurnosContext : DbContext
    {
        public ReservaTurnosContext(DbContextOptions<ReservaTurnosContext> options)
            : base(options)
        {
        }
        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comercio>(entity =>
            {
                entity.ToTable("Comercios");
                entity.HasKey(e => e.IdComercio);
                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");
                entity.Property(e => e.NombreComercio)
                .HasColumnName("nom_comercio")
                .HasMaxLength(100)
                .IsRequired();
                entity.Property(e => e.AforoMaximo).HasColumnName("aforo_maximo");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicios");
                entity.HasKey(e => e.IdServicio);
                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");
                entity.Property(e => e.NombreServicio)
                .HasColumnName("nom_servicio")
                .HasMaxLength(100)
                .IsRequired();
                entity.Property(e => e.HoraApertura).HasColumnName("hora_apertura");
                entity.Property(e => e.HoraCierre).HasColumnName("hora_cierre");
                entity.Property(e => e.Duracion).HasColumnName("duracion");
                entity.HasOne(d => d.Comercio)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdComercio);
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.ToTable("Turnos");
                entity.HasKey(e => e.IdTurno);
                entity.Property(e => e.IdTurno).HasColumnName("id_turno");
                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
                entity.Property(e => e.FechaTurno).HasColumnName("fecha_turno");
                entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
                entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
                entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasMaxLength(20)
                .HasDefaultValue(EstadoTurno.Disponible)
                .HasConversion<string>();
                entity.HasOne(d => d.Servicio)
                    .WithMany(p => p.Turnos)
                    .HasForeignKey(d => d.IdServicio);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
