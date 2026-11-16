using APIPracticas.Data;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using APIPracticas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;


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
            var invalido = Validar(alumno);
            if (invalido != null)
            {
                return BadRequest(invalido);
            }
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
            var invalido = Validar(alumnos);
            if (invalido != null)
            {
                return BadRequest(invalido);
            }
            existingAlumno.Nombre = alumnos.Nombre;
            existingAlumno.Empresa = alumnos.Empresa;
            existingAlumno.Correo = alumnos.Correo;
            existingAlumno.Carrera = alumnos.Carrera;
            existingAlumno.HorasRequeridas = alumnos.HorasRequeridas;
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
            // Load the children so EF cascades the delete; the in-memory provider only cascades tracked rows.
            var alumno = dBContext.Alumnos
                .Include(a => a.Actividades)
                .Include(a => a.Observaciones)
                .FirstOrDefault(a => a.Id == key);
            if (alumno == null)
            {
                return NotFound();
            }
            dBContext.Alumnos.Remove(alumno);
            dBContext.SaveChanges();
            return NoContent();
        }

        // The in-memory provider does not enforce the DBContext required/FK rules, so check here.
        private string? Validar(Alumnos a)
        {
            if (string.IsNullOrWhiteSpace(a.Nombre) || string.IsNullOrWhiteSpace(a.Empresa)
                || string.IsNullOrWhiteSpace(a.Correo) || string.IsNullOrWhiteSpace(a.Carrera))
                return "Nombre, empresa, correo y carrera son obligatorios.";
            if (a.HorasRequeridas <= 0) return "Las horas requeridas deben ser mayores a 0.";
            if (!dBContext.Asesores.Any(x => x.Id == a.AsesorId)) return "El asesor no existe.";
            return null;
        }
    }
}
