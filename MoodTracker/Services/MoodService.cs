using MoodTracker.Repository;
using MoodTracker.Models;
using MoodTracker.Repository.Interfaces;
using MoodTracker.Services.Interfaces;

namespace MoodTracker.Services;

public class MoodService(IMoodRepository moodRepository) : IMoodService
{
    // работа с выводом
    public async Task Create(MoodEntry moodEntry)
    {
        await moodRepository.Create(moodEntry);
    }

    public async Task<List<MoodEntry>> GetAll()
    {
        return await moodRepository.GetAll();
    }

    public async Task Update(MoodEntry moodEntry)
    {
        await moodRepository.Update(moodEntry);
    }

    public async Task Delete(long id)
    {
        await moodRepository.Delete(id);
    }

    public async Task<MoodEntry?> Get(long id)
    {
        return await moodRepository.GetEntryById(id);
    }
}