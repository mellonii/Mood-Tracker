using MoodTracker.Models;

namespace MoodTracker.Services.Interfaces;

public interface IMoodService
{
    Task Create(MoodEntry moodEntry);
    Task<List<MoodEntry>> GetAll();
    Task Update(MoodEntry moodEntry);
    Task Delete(long id);
    Task<MoodEntry?> Get(long id);
}