using Microsoft.EntityFrameworkCore;
using myProgectWebApi.DAL;
using myProgectWebApi.DAL.DTOs;
using myProgectWebApi.DAL.Repositories;
using myProgectWebApi.Services;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseInMemoryDatabase("GameDb")); 
builder.Services.AddScoped<IGameRepository, GameRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    { "Jwt:Key", "super_secret_key_123!" },
    { "Jwt:Issuer", "GameApi" },
    { "Jwt:Audience", "GameApiUsers" }
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("AllowReact");




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
