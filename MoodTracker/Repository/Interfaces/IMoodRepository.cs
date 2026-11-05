using MoodTracker.Models;

namespace MoodTracker.Repository.Interfaces;

public interface IMoodRepository
{
    Task Create(MoodEntry moodEntry);
    Task<List<MoodEntry>> GetAll();
    Task<MoodEntry?> GetEntryById(long id);
    Task Update(MoodEntry moodEntry);
    Task Delete(long id);
}