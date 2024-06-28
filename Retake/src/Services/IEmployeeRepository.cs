using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Services;

public interface IEmployeeRepository
{
    bool AddEmployee(Employee employee);

    bool UpdateEmployee(int id, AddEmployeeDto employee);
    bool DeleteEmployee(int id);
}