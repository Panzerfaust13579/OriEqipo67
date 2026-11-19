using System.ComponentModel.DataAnnotations;

namespace PracticasProfecionales.Models
{
    // Client copies of the APIPracticas models (scalar fields only; navigation properties need $expand).
    public class Alumnos
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La empresa es obligatoria.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Empresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo no válido.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Carrera { get; set; } = string.Empty;

        [Range(1, 5000, ErrorMessage = "Entre 1 y 5000 horas.")]
        public int HorasRequeridas { get; set; } = 480;

        [Required(ErrorMessage = "Selecciona un asesor.")]
        public string AsesorId { get; set; } = string.Empty;
    }

    public class Asesores
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string NumeroTelefono { get; set; } = string.Empty;
    }

    public class Actividades
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0.5, 24, ErrorMessage = "Entre 0.5 y 24 horas.")]
        public double Horas { get; set; }

        [Required(ErrorMessage = "Agrega una foto como evidencia.")]
        public string Evidencia { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        // Check-in location is mandatory: it proves the activity happened at the company.
        [Required(ErrorMessage = "Registra tu ubicación en la empresa.")]
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }

        public string AlumnoId { get; set; } = string.Empty;
    }

    public class Observaciones
    {
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "El comentario es obligatorio.")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string Comentario { get; set; } = string.Empty;

        [Range(0, 10, ErrorMessage = "Entre 0 y 10.")]
        public int Calificacion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
        public string AlumnoId { get; set; } = string.Empty;
    }

    // Result of the AI competency analysis (api/competencias/{alumnoId}).
    public record CompetenciaResultado(string Nombre, int Actividades, double Horas, string Nivel, List<string> Evidencias, string? Sugerencia);

    public record MapaCompetencias(string Resumen, List<CompetenciaResultado> Competencias, int ActividadesSinClasificar);
}
