using System.ComponentModel.DataAnnotations;

namespace EntekhabSalary.WebApi.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public decimal BasicSalary { get; set; }
    [Required]
    public decimal Allowance { get; set; }
    [Required]
    public decimal Transportation { get; set; }
    
    public override string ToString()
    {
        return $"Id: {Id}, Name: {FirstName} {LastName}, Basic Salary: {BasicSalary}, Allowance: {Allowance}, Transportation: {Transportation}";
    }
}
