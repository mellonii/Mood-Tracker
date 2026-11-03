using MoodTracker.Models;
using Dapper;
using MoodTracker.ConnectionFactory.Interfaces;

namespace MoodTracker.Repository;
                                                                                                                                                                                                                                                                                                      public class MoodRepository
{
    // работа с данными
    private readonly IDbConnectionFactory _factory;

    public MoodRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    
    internal void Create(MoodEntry moodEntry)
    {
        using var conn = _factory.Create();
        const string sql = """
                           INSERT INTO mood_entries (mood_at, mood, comment, updated_at) 
                           VALUES (@MoodAt, @Mood::mood_level, @Comment, @UpdatedAt) 
                           RETURNING id
                           """;

        var param = new
        {
            MoodAt = moodEntry.MoodAt,
            Mood = moodEntry.Mood.ToString(),
            Comment = moodEntry.Comment,
            UpdatedAt = DateTime.UtcNow
        };

        moodEntry.Id = conn.ExecuteScalar<long>(sql, param);
    }

    internal List<MoodEntry> GetAll()
    {
        using var conn = _factory.Create();
        const string sql = "SELECT * FROM mood_entries";
        var entry = conn.Query<MoodEntry>(sql);
        return entry.ToList();
    }
    
    internal MoodEntry? GetEntryById(long id)
    {
        using var conn = _factory.Create();
        const string sql = "SELECT id, mood::text AS mood, mood_at, comment, created_at, updated_at FROM mood_entries WHERE id = @Id";
        var entry = conn.QueryFirstOrDefault<MoodEntry>(sql, new { Id = id });
        return entry;
    }

    internal void Update(MoodEntry moodEntry)
    {
        using var conn = _factory.Create();
        const string sql = """
                           UPDATE mood_entries
                           SET mood_at = @MoodAt,
                               mood = @Mood::mood_level,
                               comment = @Comment,
                               updated_at = @UpdatedAt
                           WHERE id = @Id
                           """;

        // Mood передаём строкой и кастуем в SQL: Dapper отправляет enum-параметр как число
        var param = new
        {
            moodEntry.Id,
            MoodAt = moodEntry.MoodAt,
            Mood = moodEntry.Mood.ToString(),
            moodEntry.Comment,
            UpdatedAt = DateTime.UtcNow
        };

        conn.Execute(sql, param);
    }

    internal void Delete(long id)
    {
        using var conn = _factory.Create();
        const string sql = "DELETE FROM mood_entries WHERE id = @Id";
        conn.Execute(sql, new { Id = id } );
    }
}