using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniHelp.Domain.Entities;
using UniHelp.Features.Mapper;
using UniHelp.Features.Time;
using UniHelp.Features.UserFeatures.Options;
using UniHelp.Persistance.Context;
using UniHelp.Persistance.Repositories;
using UniHelp.Services.Implementation;
using UniHelp.WebAPI.Extensions;
using UniHelp.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthConfigurations(builder.Configuration);
string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("LocalConnStr")
                       ?? throw new NullReferenceException("Connection string to local DB os not set.");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("AzureConnStr")
                       ?? throw new NullReferenceException("Connection string to Azure DB os not set.");
}
builder.Services.AddDbContext<UniDataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddScopedRepositories();
builder.Services.AddScopedServices();

builder.Services.AddScopedRepositories();
builder.Services.AddScopedServices();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

builder.Services.AddScoped<ITimeProvider, TimeProvider>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey 
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UniDataContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<UniDataContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message} - An error occurred while applying the database migrations.");
    }
}

RolesDbInitializer.SeedRolesToDbAsync(app).Wait();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .WithExposedHeaders("Content-Disposition")
    .WithOrigins("http://localhost:8080", "http://localhost:4200"));

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();