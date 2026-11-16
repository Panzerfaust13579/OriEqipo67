namespace APIPracticas.Models
{
    public record CompetenciaResultado(string Nombre, int Actividades, double Horas, string Nivel, List<string> Evidencias, string? Sugerencia);

    public record MapaCompetencias(string Resumen, List<CompetenciaResultado> Competencias, int ActividadesSinClasificar);
}
