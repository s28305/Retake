using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Employee
{
    [Required(ErrorMessage = "EmpName is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "EmpName must be between 1 and 50 characters.")]
    public required string EmpName { get; set; }
    
    [Required(ErrorMessage = "JobName is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "JobName must be between 1 and 50 characters.")]
    public required string JobName { get; set; }
    
    public int ManagerId { get; set; }

    [Required(ErrorMessage = "Salary is required.")]
    public double Salary { get; set; }
    
    [Required(ErrorMessage = "Commission is required.")]
    public double Commission { get; set; }
    
    [Required(ErrorMessage = "DepId is required.")]
    public int DepId { get; set; }
    
    
    
    
}