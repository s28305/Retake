using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Services;

public interface IDepartmentRepository
{ 
    Task<IEnumerable<Department>> GetAll();
    Department? GetById(int id);
    bool AddDepartment(Department department);
}