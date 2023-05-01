namespace EntekhabSalary.WebApi.Contracts.Requests;

/// <summary>
/// Represents a request to add a new employee.
/// </summary>
public class AddEmployeeRequest
{
    /// <summary>
    /// The employee data in a serialized format.
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// The type of overtime calculator to use for the employee.
    /// </summary>
    public string OverTimeCalculator { get; set; }
}
