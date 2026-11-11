using APIPracticas.Models;
using Microsoft.EntityFrameworkCore;



namespace APIPracticas.Data

{
    public class DBContext(DbContextOptions<DbContext> options) : DbContext(options)
    {
        public DbSet<Alumnos> Alumnos { get; set; }
        public DbSet<Asesores> Asesores { get; set; }
        public DbSet<Actividades> actividades { get; set; }
        public DbSet<Observaciones> Observaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alumnos>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Empresa).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Carrera).IsRequired().HasMaxLength(200);
                entity.HasMany(e => e.Actividades)
                      .WithOne(a => a.Alumno)
                      .HasForeignKey("AlumnoId")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.Observaciones)
                      .WithOne(o => o.Alumno)
                      .HasForeignKey(o => o.AlumnoId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Asesor)
                      .WithMany(a => a.Alumnos)
                      .HasForeignKey(e => e.AsesorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Asesores>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NumeroTelefono).IsRequired().HasMaxLength(20);
                entity.HasMany(e => e.Alumnos)
                      .WithOne(a => a.Asesor)
                      .HasForeignKey(a => a.AsesorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Actividades>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Fecha).IsRequired();
                entity.HasOne(e => e.Alumno)
                      .WithMany(a => a.Actividades)
                      .HasForeignKey("AlumnoId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Observaciones>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Comentario).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Calificacion).IsRequired();
                entity.HasOne(e => e.Alumno)
                      .WithMany(a => a.Observaciones)
                      .HasForeignKey(o => o.AlumnoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
