using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Services.Departments;
using Web.Services.Email;
using Web.Services.Reminders;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? connection = configuration.GetConnectionString("Local-SqlServer");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());

        services.AddScoped<IDepartmentService, DepartmentService>();

        services.AddScoped<IReminderService, ReminderService>();

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
