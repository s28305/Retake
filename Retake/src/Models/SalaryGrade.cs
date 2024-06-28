using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class SalaryGrade
{
    [Required(ErrorMessage = "MinSalary is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "MinSalary must be a positive integer.")]
    public int MinSalary { get; set; }
    
    [Required(ErrorMessage = "MaxSalary is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "MaxSalary must be a positive integer.")]
    public int MaxSalary { get; set; }
}