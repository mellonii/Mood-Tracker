using MoodTracker.Models;

namespace MoodTracker.Repository;

public class MoodRepository
{
    // работа с данными
    internal void Create(MoodEntry moodEntry)
    {
        Console.WriteLine(moodEntry.Mood);
    }

    internal List<MoodEntry> GetAll()
    {
        return [new MoodEntry()];
    }

    internal MoodEntry GetEntryById(int id)
    {
        // return new MoodEntry(mood, comment, date, createdAt, updatedAt);
        return new MoodEntry();
    }

    internal void Update(int id, MoodEntry moodEntry)
    {
        Console.WriteLine("Отредактировано!");
    }

    internal void Delete(MoodEntry moodEntry)
    {
        Console.WriteLine("Удалено!");
    }
}