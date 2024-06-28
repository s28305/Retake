using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Department
{
    [Required(ErrorMessage = "DepName is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "DepName must be between 1 and 50 characters.")]
    public required string DepName { get; set; }
    
    [Required(ErrorMessage = "DepLocation is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "DepLocation must be between 1 and 50 characters.")]
    public required string DepLocation { get; set; }
}