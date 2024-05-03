using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.API.Configuration;
using PlaceRentalApp.API.MIddleware;
using PlaceRentalApp.Application.Services;
using PlaceRentalApp.Infraestructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PlacesConfiguration>(builder.Configuration.GetSection("PlacesConfig"));
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

//builder.Services.AddDbContext<PlaceRentalDbContext>(o => o.UseInMemoryDatabase("PlaceRentalDb"));
var connectionString = builder.Configuration.GetConnectionString("PlaceRentalCs");
builder.Services.AddDbContext<PlaceRentalDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped<IPlaceService, PlaceSevice>();
builder.Services.AddScoped<IUserService, UserService>();

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
