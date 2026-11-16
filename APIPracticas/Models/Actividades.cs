namespace APIPracticas.Models
{
    public class Actividades
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public double Horas { get; set; }
        // Photo evidence as a data URL (data:image/jpeg;base64,...).
        // shortcut: stored inline in the row, move to blob/file storage if photos make the DB or list queries heavy.
        public string Evidencia { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        // GPS check-in: proof the activity was done at the company site.
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string AlumnoId { get; set; } = string.Empty;
        public Alumnos? Alumno { get; set; }
    }
}
