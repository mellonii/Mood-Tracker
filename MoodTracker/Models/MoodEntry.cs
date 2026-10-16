namespace MoodTracker.Models;

public class MoodEntry
{
    private int _id;
    private readonly DateTimeOffset _createdAt;
    
    public DateTimeOffset Date { get; set; }
    public Mood Mood { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public MoodEntry()
    {
        Date = DateTimeOffset.Now;
        Mood = Mood.Unknown;
        Comment = "Неизвестный комментарий";
        
        _createdAt = DateTimeOffset.Now;
        UpdatedAt = DateTimeOffset.Now;
    }

    public MoodEntry(Mood mood, string comment, DateTimeOffset date)
    {
        _createdAt = DateTimeOffset.Now;
        
        Date = date;
        Mood = mood;
        Comment = comment;
        UpdatedAt = _createdAt;
    }
    
    public MoodEntry(Mood mood, string comment, DateTimeOffset date, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        _createdAt = createdAt;
        
        Date = date;
        Mood = mood;
        Comment = comment;
        UpdatedAt = updatedAt;
    }
}