namespace APIPracticas.Models
{
    public class Actividades
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public Alumnos? Alumno { get; set; }
    }
}
