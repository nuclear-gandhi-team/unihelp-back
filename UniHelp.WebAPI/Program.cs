using Microsoft.EntityFrameworkCore;
using UniHelp.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UniDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
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
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UniDataContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

RolesDbInitializer.SeedRolesToDbAsync(app).Wait();

app.UseMiddleware<ExceptionHandlerMiddleware>();

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