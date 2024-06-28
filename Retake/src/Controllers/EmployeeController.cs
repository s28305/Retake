using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController: ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Employee employee) 
    {
        var isCreated = _employeeRepository.AddEmployee(employee);
        return isCreated ? Created() : BadRequest();
    }
    
    [HttpPut(("/{id}"))]
    public IActionResult Update([FromBody] AddEmployeeDto employee, int id)
    {
        var isUpdated = _employeeRepository.UpdateEmployee(id, employee);
        return isUpdated ? NoContent() : BadRequest();
    }
    
    [HttpDelete(("/{id}"))]
    public IActionResult Delete(int id)
    {
        var isDeleted = _employeeRepository.DeleteEmployee(id);
        return isDeleted ? NoContent() : BadRequest();
    }
    
}