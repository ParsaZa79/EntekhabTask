using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Repositories.Abstract;

public interface IEmployeeRepository : IRepository<Employee>
{
    // Task<IEnumerable<Employee>> GetEmployeeInRangeAsync(int id, DateTime startDate, DateTime endDate);
}
