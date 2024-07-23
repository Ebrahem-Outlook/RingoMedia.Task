using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;

namespace Web.Services.Departments;

public class DepartmentService(IDbContext dbContext) : IDepartmentService
{
    // Commands.
    public async Task AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<Department>().AddAsync(department, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken = default)
    {
        dbContext.Set<Department>().Update(department);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Department? department = await dbContext.Set<Department>().FindAsync(id);

        if(department != null)
        {
            dbContext.Set<Department>().Remove(department);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    // Queries.
    public async Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Department>().Include(d => d.SubDepartments).ToListAsync(cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Department>().Include(d => d.SubDepartments)
                                                .FirstOrDefaultAsync(d => d.ParentDepartmentId == id, cancellationToken);
    }
}
