using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Services;

public interface IDepartmentRepository
{ 
    Task<IEnumerable<GetDeptDto>> GetAll();
    GetOneDeptDto? GetById(int id);
    bool AddDepartment(Department department);
}