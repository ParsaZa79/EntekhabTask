using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Contracts.Requests;

/// <summary>
/// Represents a request to update an existing employee.
/// </summary>
public class UpdateEmployeeRequest
{
    /// <summary>
    /// The updated employee data.
    /// </summary>
    public Employee Employee { get; set; }
}