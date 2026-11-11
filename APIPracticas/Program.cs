using APIPracticas.Data;
using APIPracticas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OData.ModelBuilder;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateSlimBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Alumnos>("Alumnos");
modelBuilder.EntitySet<Asesores>("Asesores");
modelBuilder.EntitySet<Actividades>("Actividades");
modelBuilder.EntitySet<Observaciones>("Observaciones");

builder.Services.AddControllers().AddOData(options =>
{
    options.AddRouteComponents("odata", modelBuilder.GetEdmModel());
    options.EnableQueryFeatures();
});

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseInMemoryDatabase("PracticasDB");
});

var app = builder.Build();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
    AppSeeder.Initialize(dbContext);
}

    app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
