using Microsoft.EntityFrameworkCore;
using Pokemon.Data.DataAccess;
using Pokemon.Data.DataAccess.Repositories;
using Pokemon.Services.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PokeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Pokemon"));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();