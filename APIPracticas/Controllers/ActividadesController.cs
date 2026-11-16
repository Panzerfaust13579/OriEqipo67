using APIPracticas.Data;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using APIPracticas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;


namespace APIPracticas.Controllers
{
    public class ActividadesController(DBContext dBContext):ODataController
    {
        [EnableQuery]
        public ActionResult<IQueryable<Actividades>> Get()
        {
            return Ok(dBContext.actividades);
        }

        [HttpGet("odata/Actividades({key})")]

        public ActionResult<Actividades> Get([FromRoute] string key)
        {
            var actividad = dBContext.actividades.FirstOrDefault(a => a.Id == key);
            if (actividad == null)
            {
                return NotFound();
            }
            return Ok(actividad);
        }

        public ActionResult Post([FromBody] Actividades actividad)
        {
            var invalido = Validar(actividad);
            if (invalido != null)
            {
                return BadRequest(invalido);
            }
            dBContext.actividades.Add(actividad);
            dBContext.SaveChanges();
            return Created(actividad);
        }

        public ActionResult Put([FromRoute] string key, [FromBody] Actividades actividadBody)
        {
            var existing = dBContext.actividades.FirstOrDefault(a => a.Id == key);
            if (existing == null)
            {
                return NotFound();
            }
            var invalido = Validar(actividadBody);
            if (invalido != null)
            {
                return BadRequest(invalido);
            }
            existing.Nombre = actividadBody.Nombre;
            existing.Descripcion = actividadBody.Descripcion;
            existing.Fecha = actividadBody.Fecha;
            existing.Horas = actividadBody.Horas;
            existing.Evidencia = actividadBody.Evidencia;
            existing.Latitud = actividadBody.Latitud;
            existing.Longitud = actividadBody.Longitud;
            dBContext.SaveChanges();
            return NoContent();
        }

        public ActionResult Patch([FromRoute] string key, [FromBody] Delta<Actividades> actividadDelta)
        {
            var actividad = dBContext.actividades.FirstOrDefault(a => a.Id == key);
            if (actividad == null)
            {
                return NotFound();
            }
            actividadDelta.Patch(actividad);
            dBContext.SaveChanges();
            return Updated(actividad);
        }

        public ActionResult Delete([FromRoute] string key)
        {
            var actividad = dBContext.actividades.FirstOrDefault(a => a.Id == key);
            if (actividad == null)
            {
                return NotFound();
            }
            dBContext.actividades.Remove(actividad);
            dBContext.SaveChanges();
            return NoContent();
        }

        // Log entries must carry hours, evidence and the GPS check-in.
        private static string? Validar(Actividades a)
        {
            if (a.Horas <= 0 || a.Horas > 24) return "Horas debe estar entre 0 y 24.";
            if (string.IsNullOrWhiteSpace(a.Evidencia)) return "La evidencia es obligatoria.";
            if (a.Latitud is null || a.Longitud is null) return "La ubicación GPS es obligatoria.";
            return null;
        }
    }
}
