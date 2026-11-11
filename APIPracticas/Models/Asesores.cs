namespace APIPracticas.Models
{
    public class Asesores
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string NumeroTelefono { get; set; } = string.Empty;
        public ICollection<Alumnos> Alumnos { get; set; } = [];

    }
}
