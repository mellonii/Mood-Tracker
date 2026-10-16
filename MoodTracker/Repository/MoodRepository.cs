using MoodTracker.Models;

namespace MoodTracker.Repository;

public class MoodRepository
{
    // работа с данными
    internal void Add(MoodEntry moodEntry)
    {
        Console.WriteLine(moodEntry.Mood);
    }

    internal MoodEntry[] AllEntries()
    {
        return [new MoodEntry()];
    }

    internal MoodEntry GetEntryById(int id)
    {
        // return new MoodEntry(mood, comment, date, createdAt, updatedAt);
        return new MoodEntry();
    }

    internal void Edit(MoodEntry moodEntry)
    {
        Console.WriteLine("Отредактировано!");
    }

    internal void Delete(MoodEntry moodEntry)
    {
        Console.WriteLine("Удалено!");
    }
}