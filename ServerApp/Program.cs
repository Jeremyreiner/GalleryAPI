using Gallery.DataBase.Infrastructure.MySql;
using Gallery.DataBase.Repositories;
using Gallery.Shared.Interface;
using Gallery.Shared.Services;
using GalleryAPI.Services;
using Microsoft.OpenApi.Models;
using GalleryAPI.IdentifyTokenService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

//Auth
builder.Services.AddTokenAuthentication(builder.Configuration);
//Swagger Auth
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

//MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
        new MySqlServerVersion(ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql")))));

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

// Add the JwtBearer authentication middleware to the pipeline.
app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowLocalhost");

app.MapControllers();

app.Run();