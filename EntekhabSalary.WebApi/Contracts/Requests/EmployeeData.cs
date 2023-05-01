namespace EntekhabSalary.WebApi.Contracts.Requests;

public record EmployeeData(string FirstName,
    string LastName,
    decimal BasicSalary,
    decimal Allowance,
    decimal Transportation,
    DateTime Date);