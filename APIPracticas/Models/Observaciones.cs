namespace APIPracticas.Models
{
    public class Observaciones
    {
        public string Id { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
        public int Calificacion { get; set; } = 0;
        public string AlumnoId { get; set; } = string.Empty;
        public Alumnos? Alumno { get; set; }


    }
}
