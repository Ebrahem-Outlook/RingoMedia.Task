namespace Web.Models;

public sealed class Department
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = default!;
    public string DepartmentLogo { get; set; } = default!;


    public Guid? ParentDepartmentId { get; set; }
    public Department ParentDepartment { get; set; } = default!;

    public ICollection<Department> SubDepartments { get; set; } = [];   
}
