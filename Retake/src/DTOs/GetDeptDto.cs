using WebApplication2.Models;

namespace WebApplication2.DTOs;

public class GetDeptDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }

    public List<Employee> Employees { get; set; } = new();
}