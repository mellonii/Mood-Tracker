namespace MoodTracker.Models;

public class MoodEntry
{
    public long Id { get; set; }
    public DateTimeOffset MoodAt { get; set; }
    public Mood Mood { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public MoodEntry()
    {
        Mood = Mood.Neutral;
        Comment = "Неизвестный комментарий";
        
        var dateTime = DateTimeOffset.Now.ToUniversalTime();
        MoodAt = dateTime;
        CreatedAt = dateTime;
        UpdatedAt = dateTime;
    }
    
    public MoodEntry(long id, Mood mood, string comment = "", DateTimeOffset moodAt = default, DateTimeOffset createdAt = default, DateTimeOffset updatedAt = default)
    {
        Id = id;
        
        Mood = mood;
        Comment = comment;
        
        var dateTime = DateTimeOffset.Now.ToUniversalTime();
        MoodAt = moodAt != default ? moodAt.ToUniversalTime() : dateTime;
        CreatedAt = createdAt != default ? createdAt.ToUniversalTime() : dateTime;
        UpdatedAt = updatedAt != default ? updatedAt.ToUniversalTime() : dateTime;
    }

    public MoodEntry(Mood mood, string comment = "", DateTimeOffset moodAt = default, DateTimeOffset createdAt = default, DateTimeOffset updatedAt = default)
    {
        Mood = mood;
        Comment = comment;

        var dateTime = DateTimeOffset.Now.ToUniversalTime();
        MoodAt = moodAt != default ? moodAt.ToUniversalTime() : dateTime;
        CreatedAt = createdAt != default ? createdAt.ToUniversalTime() : dateTime;
        UpdatedAt = updatedAt != default ? updatedAt.ToUniversalTime() : dateTime;
    }
}