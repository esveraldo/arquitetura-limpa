using Microsoft.OpenApi.Models;
using PlaceRentalApp.API.ApiModule;
using PlaceRentalApp.API.Configuration;
using PlaceRentalApp.API.MIddleware;
using PlaceRentalApp.Application;
using PlaceRentalApp.Infraestructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Arquitetura limpa", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Arquitetura limpa",
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//        new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference
//            {
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//            }
//        },
//        new string[] {}
//        }

//    });
//});

builder.Services.Configure<PlacesConfiguration>(builder.Configuration.GetSection("PlacesConfig"));
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

//builder.Services.AddDbContext<PlaceRentalDbContext>(o => o.UseInMemoryDatabase("PlaceRentalDb"));

builder.Services.AddApplication().AddInfraestructure(builder.Configuration);
builder.Services.AddSwaggerDoc();
builder.Services.AddCorsPolicy();

var app = builder.Build();

app.UseExceptionHandler();

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
