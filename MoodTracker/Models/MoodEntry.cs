namespace MoodTracker.Models;

internal class MoodEntry
{
    private int _id;
    private readonly DateTimeOffset _createdAt;
    
    public DateTimeOffset Date;
    public Mood Mood;
    public string Comment;
    public DateTimeOffset UpdatedAt;

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