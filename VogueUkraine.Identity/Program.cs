using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VogueUkraine.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<IdentityContext>(opt =>
{
    var connectionString = builder.Configuration.GetValue<string>("postgre:connectionString");
    opt.UseNpgsql(connectionString);
});

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<IdentityContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();