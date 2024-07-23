namespace Web.Models;

public sealed class Reminder
{
    private Reminder() { }

    public Guid ReminderId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ReminderTime { get; } 
    public string Email { get; set; } = default!;
    public bool IsSent { get; set; }
}
