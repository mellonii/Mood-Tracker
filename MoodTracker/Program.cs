// Настраивает службы и конвейер HTTP-запроса приложения и содержит управляемую точку входа приложения.
// csproj	Содержит метаданные конфигурации для проекта.
// http	Содержит конфигурацию для тестирования ИНТЕРФЕЙСов REST API.

using MoodTracker.Repository;
using MoodTracker.Services;
using MoodTracker.ConnectionFactory;
using MoodTracker.ConnectionFactory.Interfaces;
using MoodTracker.Extensions;
using MoodTracker.Models;
using Npgsql;
using Npgsql.NameTranslation;

var builder = WebApplication.CreateBuilder(args);

DatabaseSetup.InitializeMappings();

var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? throw new InvalidOperationException("Connection string 'Default' not found");
// Создаем и настраиваем источник данных с маппингом enum
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.MapEnum<Mood>("mood_level", new NpgsqlNullNameTranslator());
var dataSource = dataSourceBuilder.Build();

// Фабрика — Singleton, потому что у неё нет состояния кроме строки
builder.Services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(dataSource));

builder.Services.AddScoped<MoodRepository>();
builder.Services.AddScoped<MoodService>();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

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