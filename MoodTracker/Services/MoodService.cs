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

    internal void Update(MoodEntry moodEntry)
    {
        moodRepository.Update(moodEntry);
    }

    internal void Delete(long id)
    {
        moodRepository.Delete(id);
    }

    internal MoodEntry? Get(long id)
    {
        return moodRepository.GetEntryById(id);
    }
}