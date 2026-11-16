using APIPracticas.Data;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using APIPracticas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;


namespace APIPracticas.Controllers
{
    public class ObservacionesController(DBContext dBContext):ODataController
    {
        [EnableQuery]
        public ActionResult<IQueryable<Observaciones>> Get()
        {
            return Ok(dBContext.Observaciones);
        }

        [HttpGet("odata/Observaciones({key})")]

        public ActionResult<Observaciones> Get([FromRoute] string key)
        {
            var observacion = dBContext.Observaciones.FirstOrDefault(a => a.Id == key);
            if (observacion == null)
            {
                return NotFound();
            }
            return Ok(observacion);
        }

        public ActionResult Post([FromBody] Observaciones observacion)
        {
            if (string.IsNullOrWhiteSpace(observacion.Comentario) || observacion.Calificacion is < 0 or > 10)
            {
                return BadRequest("Comentario obligatorio y calificación entre 0 y 10.");
            }
            dBContext.Observaciones.Add(observacion);
            dBContext.SaveChanges();
            return Created(observacion);
        }

        public ActionResult Put([FromRoute] string key, [FromBody] Observaciones observacionBody)
        {
            var existing = dBContext.Observaciones.FirstOrDefault(a => a.Id == key);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Comentario = observacionBody.Comentario;
            existing.Calificacion = observacionBody.Calificacion;
            existing.AlumnoId = observacionBody.AlumnoId;
            dBContext.SaveChanges();
            return NoContent();
        }

        public ActionResult Patch([FromRoute] string key, [FromBody] Delta<Observaciones> observacionDelta)
        {
            var observacion = dBContext.Observaciones.FirstOrDefault(a => a.Id == key);
            if (observacion == null)
            {
                return NotFound();
            }
            observacionDelta.Patch(observacion);
            dBContext.SaveChanges();
            return Updated(observacion);
        }

        public ActionResult Delete([FromRoute] string key)
        {
            var observacion = dBContext.Observaciones.FirstOrDefault(a => a.Id == key);
            if (observacion == null)
            {
                return NotFound();
            }
            dBContext.Observaciones.Remove(observacion);
            dBContext.SaveChanges();
            return NoContent();
        }
    }
}
