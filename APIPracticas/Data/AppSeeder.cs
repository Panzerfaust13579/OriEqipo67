using APIPracticas.Models;

namespace APIPracticas.Data
{
    public static class AppSeeder
    {
        public static void Initialize(DBContext dbContext)
        {
            // Seed only an empty database so restarts against a persistent DB don't duplicate keys.
            if (!dbContext.Asesores.Any())
            {
                var asesores = new List<Asesores>
                {
                    new() { Id = "AS1", Nombre = "Laura Martínez", Correo = "laura.martinez@universidad.edu", NumeroTelefono = "5512345678" },
                    new() { Id = "AS2", Nombre = "Carlos Ramírez", Correo = "carlos.ramirez@universidad.edu", NumeroTelefono = "5587654321" },
                    new() { Id = "AS3", Nombre = "Sofía Torres", Correo = "sofia.torres@universidad.edu", NumeroTelefono = "5555555555" },
                    new() { Id = "AS4", Nombre = "Miguel Sánchez", Correo = "miguel.sanchez@universidad.edu", NumeroTelefono = "5511111111" },

                };

                // Actividades are attached through the Alumno's collection, so EF fills their AlumnoId.
                var alumnos = new List<Alumnos>
                {
                    new()
                    {
                        Id = "AL1", Nombre = "Ana López", Empresa = "Tech Solutions", Correo = "ana.lopez@alumnos.edu",
                        Carrera = "Ingeniería en Sistemas", AsesorId = "AS1",
                        Actividades =
                        [
                            new() { Id = "AC1", Horas = 8, Nombre = "Inducción", Descripcion = "Capacitación inicial en la empresa", Fecha = new DateTime(2026, 9, 1) },
                            new() { Id = "AC2", Horas = 6, Nombre = "Desarrollo de API", Descripcion = "Endpoints REST para inventario", Fecha = new DateTime(2026, 9, 15) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB1", Comentario = "Buen desempeño en la primera etapa", Calificacion = 9 },
                        ],
                    },
                    new()
                    {
                        Id = "AL2", Nombre = "Jorge Hernández", Empresa = "Grupo Industrial Norte", Correo = "jorge.hernandez@alumnos.edu",
                        Carrera = "Ingeniería Industrial", AsesorId = "AS1",
                        Actividades =
                        [
                            new() { Id = "AC3", Horas = 5, Nombre = "Análisis de procesos", Descripcion = "Levantamiento de la línea de producción", Fecha = new DateTime(2026, 9, 8) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB2", Comentario = "Debe mejorar la entrega de reportes", Calificacion = 7 },
                        ],
                    },
                    new()
                    {
                        Id = "AL3", Nombre = "María González", Empresa = "Consultora Digital", Correo = "maria.gonzalez@alumnos.edu",
                        Carrera = "Licenciatura en Informática", AsesorId = "AS2",
                        Actividades =
                        [
                            new() { Id = "AC4", Horas = 7, Nombre = "Diseño de interfaz", Descripcion = "Prototipos para la app móvil", Fecha = new DateTime(2026, 9, 10) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB3", Comentario = "Excelente iniciativa y comunicación", Calificacion = 10 },
                        ],
                    },
                    new()
                    {
                        Id = "AL4", Nombre = "Carlos Ramírez", Empresa = "Innovación Tecnológica", Correo = "carlos.ramirez@alumnos.edu",
                        Carrera = "Ingeniería en Sistemas", AsesorId = "AS4",
                        Actividades =
                        [
                            new() { Id = "AC5", Horas = 6, Nombre = "Implementación de base de datos", Descripcion = "Diseño y desarrollo de la BD", Fecha = new DateTime(2026, 9, 20) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB4", Comentario = "Buen trabajo en equipo", Calificacion = 8 },
                        ],
                    },
                    new()
                    {
                        Id = "AL5", Nombre = "Sofía Torres", Empresa = "Soluciones Creativas", Correo = "sofia.torres@alumnos.edu",
                        Carrera = "Licenciatura en Informática", AsesorId = "AS3",
                        Actividades =
                        [
                            new() { Id = "AC6", Horas = 8, Nombre = "Desarrollo de software", Descripcion = "Implementación de la aplicación web", Fecha = new DateTime(2026, 9, 25) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB5", Comentario = "Excelente desempeño en el proyecto", Calificacion = 9 },
                        ],
                    },
                    new()
                    {
                        Id = "AL6", Nombre = "Miguel Sánchez", Empresa = "Consultoría Empresarial", Correo = "miguel.sanchez@alumnos.edu",
                        Carrera = "Ingeniería en Sistemas", AsesorId = "AS4",
                        Actividades =
                        [
                            new() { Id = "AC7", Horas = 5, Nombre = "Análisis de requisitos", Descripcion = "Recolección y documentación de requisitos", Fecha = new DateTime(2026, 10, 5) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB6", Comentario = "Buen desempeño en el análisis", Calificacion = 7 },
                        ],
                    },
                    new()
                    {
                        Id = "AL7", Nombre = "Lucía Fernández", Empresa = "Desarrollo de Software Avanzado", Correo = "lucia.fernandez@alumnos.edu",
                        Carrera = "Licenciatura en Informática", AsesorId = "AS3",
                        Actividades =
                        [
                            new() { Id = "AC8", Horas = 7, Nombre = "Diseño de interfaz de usuario", Descripcion = "Creación de mockups y prototipos", Fecha = new DateTime(2026, 10, 10) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB7", Comentario = "Excelente trabajo en el diseño", Calificacion = 9 },
                        ],
                    },
                    new()
                    {
                        Id = "AL8", Nombre = "Diego Martínez", Empresa = "Innovación y Tecnología", Correo = "diego.martinez@alumnos.edu",
                        Carrera = "Ingeniería en Sistemas", AsesorId = "AS4",
                        Actividades =
                        [
                            new() { Id = "AC9", Horas = 6, Nombre = "Implementación de funcionalidades", Descripcion = "Desarrollo e implementación de características", Fecha = new DateTime(2026, 10, 15) },
                        ],
                        Observaciones =
                        [
                            new() { Id = "OB8", Comentario = "Buen desempeño en la implementación", Calificacion = 8 },
                        ],
                    }
                };
                dbContext.Asesores.AddRange(asesores);
                dbContext.Alumnos.AddRange(alumnos);
                dbContext.SaveChanges();

            }
        }
    } 
}

            
        