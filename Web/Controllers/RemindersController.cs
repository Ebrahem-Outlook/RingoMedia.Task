using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services.Reminders;

namespace Web.Controllers;

public sealed class RemindersController(IReminderService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReminders()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetReminder(Guid id)
    {
        Reminder? reminder = await service.GetByIdAsync(id);

        if(reminder is null)
        {
            return NotFound();
        }

        return Ok(reminder);
    }

    [HttpPost]
    public async Task<IActionResult> AddReminder([FromBody]Reminder reminder)
    {
        await service.AddAsync(reminder);

        return CreatedAtAction(nameof(GetReminder), new { id = reminder.ReminderId }, reminder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReminder(Guid id, [FromBody] Reminder reminder)
    {
        if (id != reminder.ReminderId)
        {
            return BadRequest();
        }
        await service.UpdateAsync(reminder);
        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteReminder(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("send-emails")]
    public async Task<IActionResult> SendReminderAsync()
    {
        await service.SendReminderEmailsAsync();
        return NoContent();
    }
}
