using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data.DataAccess;
using Pokemon.Data.DataAccess.Repositories;
using Pokemon.Services.Core;
using Pokemon.Web.API;
using Pokemon.Web.API.Middleware;

[assembly:InternalsVisibleTo("Pokemon.Web.API.Tests")]

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddEnvironmentVariables();
    config.AddJsonFile("appsettings.json");
    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json");
});

// Add services to the container.
builder.Services.AddDbContext<PokeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Pokemon").AddPortToConnectionString());
});
builder.Services.AddScoped<PokeRepository>();
builder.Services.AddScoped<PokemonService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(PokemonErrorHandler.HandleErrors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();