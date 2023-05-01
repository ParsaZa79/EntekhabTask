using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntekhabSalary.WebApi.Models;

public class EmployeeSalary
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public DateTime SalaryDate { get; set; }
    [Required]
    public decimal TotalSalary { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee Employee { get; set; }
}