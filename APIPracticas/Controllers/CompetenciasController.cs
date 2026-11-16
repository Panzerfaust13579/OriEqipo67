using APIPracticas.Data;
using APIPracticas.Models;
using APIPracticas.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIPracticas.Controllers
{
    // Plain (non-OData) endpoint: the analysis is a computed result, not an entity set.
    [ApiController]
    [Route("api/competencias")]
    public class CompetenciasController(DBContext dBContext) : ControllerBase
    {
        [HttpGet("{alumnoId}")]
        public ActionResult<MapaCompetencias> Get([FromRoute] string alumnoId)
        {
            if (!dBContext.Alumnos.Any(a => a.Id == alumnoId))
            {
                return NotFound();
            }
            // Project only the text and hours so the base64 photos are not loaded.
            var actividades = dBContext.actividades
                .Where(a => a.AlumnoId == alumnoId)
                .Select(a => new { a.Nombre, a.Descripcion, a.Horas })
                .AsEnumerable()
                .Select(a => (a.Nombre, a.Descripcion, a.Horas));
            return Ok(ClasificadorCompetencias.Analizar(actividades));
        }
    }
}
