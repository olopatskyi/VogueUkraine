using System.Reflection;
using Microsoft.OpenApi.Models;
using VogueUkraine.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.SetUpConfigurations(builder.Environment);
builder.ConfigureKestrel();

builder.Services.AddControllers();
builder.Services.SetUpAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });                
});

builder.Services
    .AddConfigurations(builder.Configuration)
    .AddDatabase()
    .AddManagers()
    .AddServices()
    .AddRepositories()
    .AddAutoMapper(Assembly.GetExecutingAssembly());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VogueUkraine.Identity");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Run();