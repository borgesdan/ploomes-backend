using Microsoft.EntityFrameworkCore;
using Ploomes.API.Extensions;
using Ploomes.Application.Data.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("default"),
        //Como requisitado pelo Package Manager Console
        b => b.MigrationsAssembly("Ploomes.API")));

builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName(), true);

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddAppServices();
builder.Services.AddAppRepositories();

//Adiciona Cors Policy padrão.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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
