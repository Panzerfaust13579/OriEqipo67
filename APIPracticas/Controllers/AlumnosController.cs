using APIPracticas.Data;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using APIPracticas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;


namespace APIPracticas.Controllers
{
    public class AlumnosController(DBContext dBContext):ODataController
    {
        [EnableQuery]
        public ActionResult<IQueryable<Alumnos>> Get()
        {
            return Ok(dBContext.Alumnos);
        }

        [HttpGet("odata/Alumnos({key})")]

        public ActionResult<Alumnos> Get([FromRoute] string key)
        {
            var alumno = dBContext.Alumnos.FirstOrDefault(a => a.Id == key);
            if (alumno == null)
            {
                return NotFound();
            }
            return Ok(alumno);
        }

        public ActionResult Post([FromBody] Alumnos alumno)
        {
            dBContext.Alumnos.Add(alumno);
            dBContext.SaveChanges();
            return Created(alumno);
        }

        public ActionResult Put([FromRoute] string key, [FromBody] Alumnos alumnos) 
        {
            var existingAlumno = dBContext.Alumnos.FirstOrDefault(a => a.Id == key);
            if (existingAlumno == null)
            {
                return NotFound();
            }
            existingAlumno.Nombre = alumnos.Nombre;
            existingAlumno.Empresa = alumnos.Empresa;
            existingAlumno.Correo = alumnos.Correo;
            existingAlumno.Carrera = alumnos.Carrera;
            existingAlumno.AsesorId = alumnos.AsesorId;
            dBContext.SaveChanges();
            return NoContent();

        }

        public ActionResult Patch([FromRoute] string key, [FromBody] Delta<Alumnos> alumnoDelta)
        {
            var alumno = dBContext.Alumnos.FirstOrDefault(a => a.Id == key);
            if (alumno == null)
            {
                return NotFound();
            }
            alumnoDelta.Patch(alumno);
            dBContext.SaveChanges();
            return Updated(alumno);
        }
        public ActionResult Delete([FromRoute] string key)
        {
            var alumno = dBContext.Alumnos.FirstOrDefault(a => a.Id == key);
            if (alumno == null)
            {
                return NotFound();
            }
            dBContext.Alumnos.Remove(alumno);
            dBContext.SaveChanges();
            return NoContent();
        }
    }
}
