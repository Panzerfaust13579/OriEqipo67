using APIPracticas.Models;
using System.Text.RegularExpressions;

namespace APIPracticas.Services
{
    // MOCK of the AI component (see docs/PropuestaIA.md).
    // The real version sends the activity texts to Azure OpenAI with a JSON schema of these labels
    // (no training needed; Azure AI Language custom classification is the trained alternative)
    // and gets back { category, confidenceScore } per label. This mock keeps that same output shape
    // but decides the labels with keyword rules, so swapping in the real call only changes Clasificar().
    public static class ClasificadorCompetencias
    {
        public record Competencia(string Nombre, string[] Palabras, string Sugerencia);
        public record Clasificacion(string Categoria, double Confianza);

        // Graduate profile = the label schema sent to the classifier. Keywords are lowercase, without accents.
        public static readonly Competencia[] Perfil =
        [
            new("Desarrollo de software",
                ["api", "codigo", "desarrollo", "programa", "endpoint", "software", "base de datos", "backend", "frontend", "app"],
                "Asignar una tarea de programación real: un módulo, endpoint o corrección de bugs con revisión de código."),
            new("Análisis y mejora de procesos",
                ["analisis", "proceso", "produccion", "levantamiento", "optimiza", "mejora", "indicador", "diagnostico"],
                "Pedir que documente un proceso de la empresa y proponga una mejora medible con indicadores."),
            new("Diseño y experiencia de usuario",
                ["diseno", "interfaz", "prototipo", "ux", "ui", "usabilidad", "maqueta", "wireframe"],
                "Incluirlo en el diseño o prueba de usabilidad de una pantalla, con entrevistas a usuarios."),
            new("Comunicación y trabajo en equipo",
                ["reunion", "presentacion", "equipo", "cliente", "reporte", "documentacion", "exposicion", "informe"],
                "Pedir que presente avances al equipo o al cliente y entregue un reporte escrito."),
            new("Gestión de proyectos",
                ["planeacion", "cronograma", "scrum", "gestion", "seguimiento", "requisitos", "requerimientos", "sprint", "estimacion"],
                "Darle la planeación de un sprint o un cronograma, con seguimiento semanal de tareas."),
            new("Ética, seguridad y aprendizaje continuo",
                ["capacitacion", "induccion", "curso", "normativa", "seguridad", "certificacion", "politica", "etica"],
                "Asignar una capacitación o certificación y una reflexión sobre normas y ética de la empresa."),
        ];

        // Self-check of the mock rules; runs once at startup in Debug builds.
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Verificar()
        {
            System.Diagnostics.Debug.Assert(Clasificar("Reunión con el equipo de seguimiento").Select(c => c.Categoria)
                .SequenceEqual(["Comunicación y trabajo en equipo", "Gestión de proyectos"]));
            System.Diagnostics.Debug.Assert(Clasificar("Trabajo rápido").Count == 0);
            System.Diagnostics.Debug.Assert(Analizar([("Diseño de interfaz", "", 4)]).Competencias.Count(c => c.Actividades == 0) == Perfil.Length - 1);
        }

        public static List<Clasificacion> Clasificar(string texto)
        {
            // Match keywords at word start only, so "ui" does not hit "equipo" nor "api" hit "rapido".
            var t = " " + Regex.Replace(SinAcentos(texto.ToLowerInvariant()), "[^a-z0-9]+", " ");
            return Perfil
                .Select(c => (c.Nombre, Aciertos: c.Palabras.Count(p => t.Contains(" " + p))))
                .Where(x => x.Aciertos > 0)
                .Select(x => new Clasificacion(x.Nombre, Math.Min(0.99, 0.6 + 0.15 * x.Aciertos)))
                .ToList();
        }

        public static MapaCompetencias Analizar(IEnumerable<(string Nombre, string Descripcion, double Horas)> actividades)
        {
            var clasificadas = actividades.Select(a => (a, Etiquetas: Clasificar($"{a.Nombre}. {a.Descripcion}"))).ToList();

            var competencias = Perfil.Select(c =>
            {
                var propias = clasificadas.Where(x => x.Etiquetas.Any(e => e.Categoria == c.Nombre)).ToList();
                var horas = propias.Sum(x => x.a.Horas);
                return new CompetenciaResultado(
                    c.Nombre,
                    propias.Count,
                    horas,
                    Nivel(horas),
                    propias.Select(x => x.a.Nombre).ToList(),
                    propias.Count == 0 ? c.Sugerencia : null);
            }).ToList();

            var brechas = competencias.Where(c => c.Actividades == 0).Select(c => c.Nombre).ToList();
            var resumen = brechas.Count == 0
                ? "El alumno tiene evidencia en todas las competencias del perfil de egreso."
                : $"El alumno desarrolla {competencias.Count - brechas.Count} de {competencias.Count} competencias. Sin evidencia: {string.Join(", ", brechas)}.";

            return new MapaCompetencias(resumen, competencias, clasificadas.Count(x => x.Etiquetas.Count == 0));
        }

        private static string Nivel(double horas) => horas switch
        {
            0 => "Sin evidencia",
            < 20 => "Inicial",
            < 80 => "En desarrollo",
            _ => "Consolidado",
        };

        // InvariantGlobalization is on, so avoid string.Normalize and strip the Spanish accents directly.
        private static string SinAcentos(string s) =>
            s.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace('ü', 'u').Replace('ñ', 'n');
    }
}
