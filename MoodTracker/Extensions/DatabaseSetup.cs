using Dapper;

namespace MoodTracker.Extensions;

public static class DatabaseSetup
{
    public static void InitializeMappings()
    {
        // Dapper: колонка mood_at -> свойство MoodAt, created_at -> CreatedAt и т.д.
        // (маппинг PostgreSQL-enum Mood делается в Program.cs на NpgsqlDataSourceBuilder)
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
