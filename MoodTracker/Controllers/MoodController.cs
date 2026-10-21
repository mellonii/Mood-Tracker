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

    [HttpPost]
    public IActionResult Create(MoodEntry moodEntry)
    {            
        _moodService.Create(moodEntry);
        return CreatedAtAction(nameof(Get), new { id = moodEntry.id }, moodEntry);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, MoodEntry moodEntry)
    {
        if (id != moodEntry.id)
            return BadRequest();
           
        var existingEntry = _moodService.Get(id);
        if(existingEntry is null) return NotFound();
   
        _moodService.Update(id, moodEntry);           
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var moodEntry = _moodService.Get(id);
        if (moodEntry is null) return NotFound();
       
        _moodService.Delete(id);
        return NoContent();
    }
}