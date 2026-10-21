using MoodTracker.Repository;
using MoodTracker.Models;

namespace MoodTracker.Services;

public class MoodService(MoodRepository moodRepository)
{
    // работа с выводом
    internal void Create(MoodEntry moodEntry)
    {
        moodRepository.Create(moodEntry);
    }

    internal List<MoodEntry> GetAll()
    {
        return moodRepository.GetAll();
    }

    internal void Update(int id, MoodEntry moodEntry)
    {
        moodRepository.Update(id, moodEntry);
    }

    internal void Delete(int id)
    {
        var moodEntry = moodRepository.GetEntryById(id);
        moodRepository.Delete(moodEntry);
    }

    internal MoodEntry Get(int id)
    {
        return moodRepository.GetEntryById(id);
    }
}