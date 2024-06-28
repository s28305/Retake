using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication2.DTOs;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    [HttpGet]
    public Task<IEnumerable<Department>> GetAll()
    {
        return _departmentRepository.GetAll();
    }
    
    [HttpGet("/{id}")]
    public IActionResult Get(int id) 
    {
        var exists = _departmentRepository.GetById(id) != null;
        return exists ? Ok(_departmentRepository.GetById(id)) : BadRequest(); 
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Department department) 
    {
        var isCreated = _departmentRepository.AddDepartment(department);
        return isCreated ? Created() : BadRequest();
    }
    
    
}