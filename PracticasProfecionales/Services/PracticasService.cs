using System.Net.Http.Json;
using System.Text.Json;
using PracticasProfecionales.Models;

namespace PracticasProfecionales.Services
{
    // Talks to APIPracticas OData endpoints: odata/{set} and odata/{set}('{key}').
    public class PracticasService(HttpClient httpClient)
    {
        // OData payloads use the C# property names as-is (PascalCase), so don't camelCase on send.
        private static readonly JsonSerializerOptions SendOptions = new();

        public Task<List<Alumnos>> GetAlumnosAsync() => GetAllAsync<Alumnos>("Alumnos");
        public Task<Alumnos?> GetAlumnoAsync(string id) => GetAsync<Alumnos>("Alumnos", id);
        public Task CreateAlumnoAsync(Alumnos alumno) => CreateAsync("Alumnos", alumno);
        public Task UpdateAlumnoAsync(Alumnos alumno) => UpdateAsync("Alumnos", alumno.Id, alumno);
        public Task DeleteAlumnoAsync(string id) => DeleteAsync("Alumnos", id);

        public Task<List<Asesores>> GetAsesoresAsync() => GetAllAsync<Asesores>("Asesores");
        public Task<Asesores?> GetAsesorAsync(string id) => GetAsync<Asesores>("Asesores", id);
        public Task CreateAsesorAsync(Asesores asesor) => CreateAsync("Asesores", asesor);
        public Task UpdateAsesorAsync(Asesores asesor) => UpdateAsync("Asesores", asesor.Id, asesor);
        public Task DeleteAsesorAsync(string id) => DeleteAsync("Asesores", id);

        public Task<List<Actividades>> GetActividadesAsync() => GetAllAsync<Actividades>("Actividades");
        public Task<List<Actividades>> GetActividadesByAlumnoAsync(string alumnoId) =>
            GetAllAsync<Actividades>($"Actividades?$filter=AlumnoId eq '{Uri.EscapeDataString(alumnoId.Replace("'", "''"))}'&$orderby=Fecha desc");
        public Task<Actividades?> GetActividadAsync(string id) => GetAsync<Actividades>("Actividades", id);
        public Task CreateActividadAsync(Actividades actividad) => CreateAsync("Actividades", actividad);
        public Task UpdateActividadAsync(Actividades actividad) => UpdateAsync("Actividades", actividad.Id, actividad);
        public Task DeleteActividadAsync(string id) => DeleteAsync("Actividades", id);

        public Task<List<Observaciones>> GetObservacionesAsync() => GetAllAsync<Observaciones>("Observaciones");
        public Task<List<Observaciones>> GetObservacionesByAlumnoAsync(string alumnoId) =>
            GetAllAsync<Observaciones>($"Observaciones?$filter=AlumnoId eq '{Uri.EscapeDataString(alumnoId.Replace("'", "''"))}'&$orderby=Fecha desc");
        public Task<Observaciones?> GetObservacionAsync(string id) => GetAsync<Observaciones>("Observaciones", id);
        public Task CreateObservacionAsync(Observaciones observacion) => CreateAsync("Observaciones", observacion);
        public Task UpdateObservacionAsync(Observaciones observacion) => UpdateAsync("Observaciones", observacion.Id, observacion);
        public Task DeleteObservacionAsync(string id) => DeleteAsync("Observaciones", id);

        // Not OData: a computed analysis endpoint.
        public Task<MapaCompetencias?> GetMapaCompetenciasAsync(string alumnoId) =>
            httpClient.GetFromJsonAsync<MapaCompetencias>($"api/competencias/{Uri.EscapeDataString(alumnoId)}");

        private async Task<List<T>> GetAllAsync<T>(string set)
        {
            // OData wraps collections as { "value": [...] }.
            var result = await httpClient.GetFromJsonAsync<ODataList<T>>($"odata/{set}");
            return result?.Value ?? [];
        }

        private async Task<T?> GetAsync<T>(string set, string id)
        {
            var response = await httpClient.GetAsync(Url(set, id));
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return default;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task CreateAsync<T>(string set, T entity)
        {
            var response = await httpClient.PostAsJsonAsync($"odata/{set}", entity, SendOptions);
            response.EnsureSuccessStatusCode();
        }

        private async Task UpdateAsync<T>(string set, string id, T entity)
        {
            var response = await httpClient.PutAsJsonAsync(Url(set, id), entity, SendOptions);
            response.EnsureSuccessStatusCode();
        }

        private async Task DeleteAsync(string set, string id)
        {
            var response = await httpClient.DeleteAsync(Url(set, id));
            response.EnsureSuccessStatusCode();
        }

        // String keys are quoted in OData; a quote inside the key is escaped by doubling it.
        private static string Url(string set, string id) =>
            $"odata/{set}('{Uri.EscapeDataString(id.Replace("'", "''"))}')";

        private class ODataList<T>
        {
            public List<T> Value { get; set; } = [];
        }
    }
}
