using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Repositories.Abstract;

public interface IEmployeeSalaryRepository
{
    Task<int> AddAsync(EmployeeSalary employeeSalary);
    public Task<IEnumerable<EmployeeSalary>> GetEmployeeInRangeAsync(int id, DateTime startDate, DateTime endDate);
}