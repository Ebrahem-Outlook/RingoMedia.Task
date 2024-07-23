using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services.Departments;

namespace Web.Controllers;

[ApiController]
[Route("v1/[Controller]")]
public sealed class DepartmentsController(IDepartmentService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> DetDepartments()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartment(Guid id)
    {
        Department? department = await service.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> AddDepartment(Department department)
    {
        await service.AddAsync(department);
        return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatedDepartment(Guid id, [FromBody]Department department)
    {
        if (id != department.DepartmentId)
        {
            return BadRequest();
        }

        await service.UpdateAsync(department);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
