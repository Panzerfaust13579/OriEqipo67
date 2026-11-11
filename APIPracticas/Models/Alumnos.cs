namespace APIPracticas.Models
{
    public class Alumnos
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public ICollection<Actividades> Actividades { get; set; } = [];
        public ICollection<Observaciones> Observaciones { get; set; } = [];
        public string AsesorId { get; set; } = string.Empty;
        public Asesores? Asesor { get; set; }

    }
}
