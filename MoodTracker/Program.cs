// Настраивает службы и конвейер HTTP-запроса приложения и содержит управляемую точку входа приложения.
// csproj	Содержит метаданные конфигурации для проекта.
// http	Содержит конфигурацию для тестирования ИНТЕРФЕЙСов REST API.

using MoodTracker.Repository;
using MoodTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<MoodService>();
builder.Services.AddScoped<MoodRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
