using Microsoft.EntityFrameworkCore;
using UniHelp.Persistance.Context;
using UniHelp.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthConfigurations(builder.Configuration);
builder.Services.AddDbContext<UniDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

RolesDbInitializer.SeedRolesToDbAsync(app).Wait();

// Configure the HTTP request pipeline.
app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .WithExposedHeaders("Content-Disposition")
    .WithOrigins("http://localhost:8080", "http://localhost:4200"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();