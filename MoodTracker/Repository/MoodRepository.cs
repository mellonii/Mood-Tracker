using MoodTracker.Models;
using Dapper;
using MoodTracker.ConnectionFactory.Interfaces;
using MoodTracker.Repository.Interfaces;

namespace MoodTracker.Repository;
public class MoodRepository : IMoodRepository {
    // работа с данными
    private readonly IDbConnectionFactory _factory;

    public MoodRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task Create(MoodEntry moodEntry)
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

        moodEntry.Id = await conn.ExecuteScalarAsync<long>(sql, param);
    }

    public async Task<List<MoodEntry>> GetAll()
    {
        using var conn = _factory.Create();
        const string sql = "SELECT * FROM mood_entries";
        var entry = await conn.QueryAsync<MoodEntry>(sql);
        return entry.ToList();
    }
    
    public async Task<MoodEntry?> GetEntryById(long id)
    {
        using var conn = _factory.Create();
        const string sql = "SELECT id, mood::text AS mood, mood_at, comment, created_at, updated_at FROM mood_entries WHERE id = @Id";
        var entry = await conn.QueryFirstOrDefaultAsync<MoodEntry>(sql, new { Id = id });
        return entry;
    }

    public async Task Update(MoodEntry moodEntry)
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

        await conn.ExecuteAsync(sql, param);
    }

    public async Task Delete(long id)
    {
        using var conn = _factory.Create();
        const string sql = "DELETE FROM mood_entries WHERE id = @Id";
        await conn.ExecuteAsync(sql, new { Id = id } );
    }
}