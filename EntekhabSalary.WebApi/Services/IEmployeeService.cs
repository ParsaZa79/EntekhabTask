using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Services;

public interface IEmployeeService
{
    Task<int?> AddEmployee(AddEmployeeRequest addEmployeeRequest, string dataType);
    Task<bool> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest);
    Task<bool> DeleteEmployee(int id);
    Task<Employee?> GetEmployee(int id);
}