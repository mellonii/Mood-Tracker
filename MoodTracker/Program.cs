// Настраивает службы и конвейер HTTP-запроса приложения и содержит управляемую точку входа приложения.
// csproj	Содержит метаданные конфигурации для проекта.
// http	Содержит конфигурацию для тестирования ИНТЕРФЕЙСов REST API.

using MoodTracker.Repository;
using MoodTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // добавляем генератор Swagger в коллекцию служб /swagger/v1/swagger.json
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MoodService>();
builder.Services.AddScoped<MoodRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // http://localhost:5000/ вместо http://localhost:5000/swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
