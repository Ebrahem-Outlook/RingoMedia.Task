using Web.Models;

namespace Web.Services.Reminders;

public interface IReminderService
{
    // Commands
    Task AddAsync(Reminder reminder, CancellationToken cancellationToken = default);
    Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // Queries.
    Task<List<Reminder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Reminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task SendReminderEmailsAsync(CancellationToken cancellationToken = default);
}
