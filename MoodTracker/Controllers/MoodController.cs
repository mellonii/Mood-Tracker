using Microsoft.AspNetCore.Mvc;
using MoodTracker.Services;
using MoodTracker.Models;

namespace MoodTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class MoodController : ControllerBase
{
    private readonly MoodService _moodService;
    
    public MoodController(MoodService moodService)
    {
        _moodService = moodService;
    }
    
    [HttpGet]
    public ActionResult<List<MoodEntry>> GetAll() =>
        _moodService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<MoodEntry> Get(int id)
    {
        var moodEntry = _moodService.Get(id);
        
        if(moodEntry == null) return NotFound();

        return moodEntry;
    }

    // POST action

    // PUT action

    // DELETE action
}