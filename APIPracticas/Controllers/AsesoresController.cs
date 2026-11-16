using APIPracticas.Data;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using APIPracticas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;


namespace APIPracticas.Controllers
{
    public class AsesoresController(DBContext dBContext):ODataController
    {
        [EnableQuery]
        public ActionResult<IQueryable<Asesores>> Get()
        {
            return Ok(dBContext.Asesores);
        }

        [HttpGet("odata/Asesores({key})")]

        public ActionResult<Asesores> Get([FromRoute] string key)
        {
            var asesor = dBContext.Asesores.FirstOrDefault(a => a.Id == key);
            if (asesor == null)
            {
                return NotFound();
            }
            return Ok(asesor);
        }

        public ActionResult Post([FromBody] Asesores asesor)
        {
            dBContext.Asesores.Add(asesor);
            dBContext.SaveChanges();
            return Created(asesor);
        }

        public ActionResult Put([FromRoute] string key, [FromBody] Asesores asesorBody)
        {
            var existing = dBContext.Asesores.FirstOrDefault(a => a.Id == key);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Nombre = asesorBody.Nombre;
            existing.Correo = asesorBody.Correo;
            existing.NumeroTelefono = asesorBody.NumeroTelefono;
            dBContext.SaveChanges();
            return NoContent();
        }

        public ActionResult Patch([FromRoute] string key, [FromBody] Delta<Asesores> asesorDelta)
        {
            var asesor = dBContext.Asesores.FirstOrDefault(a => a.Id == key);
            if (asesor == null)
            {
                return NotFound();
            }
            asesorDelta.Patch(asesor);
            dBContext.SaveChanges();
            return Updated(asesor);
        }

        public ActionResult Delete([FromRoute] string key)
        {
            var asesor = dBContext.Asesores.FirstOrDefault(a => a.Id == key);
            if (asesor == null)
            {
                return NotFound();
            }
            dBContext.Asesores.Remove(asesor);
            dBContext.SaveChanges();
            return NoContent();
        }
    }
}
