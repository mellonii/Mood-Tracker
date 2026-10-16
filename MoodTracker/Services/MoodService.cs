using MoodTracker.Repository;
using MoodTracker.Models;

namespace MoodTracker.Services;

public class MoodService(MoodRepository moodRepository)
{
    // работа с выводом
    internal void AddEntry(Mood mood, string comment, DateTimeOffset date = default)
    {
        if (date == default) date = DateTimeOffset.Now;
        moodRepository.Add(new MoodEntry(mood, comment, date));
    }

    internal MoodEntry[] AllEntries()
    {
        return moodRepository.AllEntries();
    }

    internal void EditEntry(int id, Models.Mood mood, string comment, DateTimeOffset date)
    {
        var moodEntry = moodRepository.GetEntryById(id);
        moodEntry.Comment = comment;
        moodEntry.Date = date;
        moodEntry.Mood = mood;
        moodEntry.UpdatedAt = DateTimeOffset.Now;
        moodRepository.Edit(moodEntry);
    }

    internal void DeleteEntry(int id)
    {
        var moodEntry = moodRepository.GetEntryById(id);
        moodRepository.Delete(moodEntry);
    }
}