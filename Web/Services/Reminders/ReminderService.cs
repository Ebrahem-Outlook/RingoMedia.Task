using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;
using Web.Services.Email;

namespace Web.Services.Reminders;

internal sealed class ReminderService(IDbContext dbContext, IEmailService emailService) : IReminderService
{
    // Commands.
    public async Task AddAsync(Reminder reminder, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<Reminder>().AddAsync(reminder, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken = default)
    {
        dbContext.Set<Reminder>().Update(reminder);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Reminder? reminder = await dbContext.Set<Reminder>().FindAsync(id, cancellationToken);

        if (reminder is not null)
        {
            dbContext.Set<Reminder>().Remove(reminder);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    // Queries.
    public async Task<List<Reminder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Reminder>().ToListAsync(cancellationToken);
    }

    public async Task<Reminder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Reminder>().FirstOrDefaultAsync(r => r.ReminderId == id, cancellationToken);
    }


    // Send Reminder Email...
    public async Task SendReminderEmailsAsync(CancellationToken cancellationToken)
    {
        // Fetch reminders that have not been sent.
        var remindersToSend = await dbContext.Set<Reminder>()
                                              .Where(r => !r.IsSent)
                                              .ToListAsync(cancellationToken);

        // Process each reminder.
        foreach (var reminder in remindersToSend)
        {
            // Construct eamil content...
            var subject = $"Reminder: {reminder.Title}";
            var body = $"Hi,\n\nThis is a reminder about: {reminder.Description}\n\nThank you!";

            // Send email.
            await emailService.SendEmailAsync(reminder.Email, subject, body);

            // Mark reminder as sent.
            reminder.IsSent = true;

            // Update the reminder in the database
            dbContext.Set<Reminder>().Update(reminder);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
