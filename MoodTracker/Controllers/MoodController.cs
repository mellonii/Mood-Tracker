using Microsoft.AspNetCore.Mvc;
using MoodTracker.Services;
using MoodTracker.Models;
using MoodTracker.Services.Interfaces;

namespace MoodTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class MoodController : ControllerBase
{
    private readonly IMoodService _moodService;
    
    public MoodController(IMoodService moodService)
    {
        _moodService = moodService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MoodEntry>>> GetAll()
    {
        return await _moodService.GetAll();   
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MoodEntry>> Get(long id)
    {
        var moodEntry = await _moodService.Get(id);
        
        if(moodEntry == null) return NotFound();
        return moodEntry;
    }

    [HttpPost]
    public async Task<IActionResult> Create(MoodEntry moodEntry)
    {            
        await _moodService.Create(moodEntry);
        return CreatedAtAction(nameof(Get), new { id = moodEntry.Id }, moodEntry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, MoodEntry moodEntry)
    {
        if (id != moodEntry.Id)
            return BadRequest();
           
        var existingEntry = await _moodService.Get(id);
        if(existingEntry is null) return NotFound();
   
        await _moodService.Update(moodEntry);           
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var moodEntry = await _moodService.Get(id);
        if (moodEntry is null) return NotFound();
       
        await _moodService.Delete(id);
        return NoContent();
    }
}