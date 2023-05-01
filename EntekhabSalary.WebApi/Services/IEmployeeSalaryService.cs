using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Services;

public interface IEmployeeSalaryService
{
    public Task<IEnumerable<EmployeeSalary>> GetEmployeeSalariesInRangeAsync(int id, DateTime startDate, DateTime endDate);
}