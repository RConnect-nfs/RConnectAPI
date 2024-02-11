using RconnectAPI.Models;
using RconnectAPI.Services;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
// Charger le fichier .env s'il existe
var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath);
}
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
//Scoped
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<HobbyService>();
builder.Services.AddSingleton<HostService>();
builder.Services.AddSingleton<MeetingService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();