using Carter;
using Gallery.DataBase.Infrastructure.MySql;
using Gallery.DataBase.Repositories;
using Gallery.Shared.Interface;
using Gallery.Shared.Services;
using GalleryAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Serilog configuration
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<InitializationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the JwtAuthenticationService as a singleton service.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IIdentifyTokenService, IdentifyTokenService>();
builder.Services.AddScoped<IDalService, DalService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddTokenAuthentication(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
});

builder.Services.AddHttpClient("GitHub", httpClient =>
{
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHub Explorer");
    httpClient.BaseAddress = new Uri("https://api.github.com");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
        new MySqlServerVersion(ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql")))));

builder.Services.AddCarter();
var app = builder.Build();

//AppDbContext on startup
var scope = app.Services.CreateScope();

scope.ServiceProvider.GetService<ApplicationDbContext>(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add the JwtBearer authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowLocalhost");

//map all minimal api endpoints using carter
app.MapCarter();

app.Run();