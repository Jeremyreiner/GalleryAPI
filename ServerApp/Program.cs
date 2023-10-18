using GalleryAPI.Services;
using GalleryAPI.Interface;
using Microsoft.OpenApi.Models;
using GalleryAPI.IdentifyTokenService;
using Publify.Services.IdentifyTokenService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the JwtAuthenticationService as a singleton service.
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<IIdentifyTokenService, IdentifyTokenService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IGitHubService, GitHubService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

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


var app = builder.Build();

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