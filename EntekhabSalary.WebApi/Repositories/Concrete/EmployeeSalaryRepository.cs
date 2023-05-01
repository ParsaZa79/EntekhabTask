using Dapper;
using EntekhabSalary.WebApi.Data;
using EntekhabSalary.WebApi.Models;
using EntekhabSalary.WebApi.Repositories.Abstract;

namespace EntekhabSalary.WebApi.Repositories.Concrete;

public class EmployeeSalaryRepository : IEmployeeSalaryRepository
{
    private readonly EfDbContext _efDbContext;
    private readonly DapperDbContext _dapperDbContext;

    public EmployeeSalaryRepository(EfDbContext efDbContext)
    {
        _efDbContext = efDbContext;
    }

    public async Task<int> AddAsync(EmployeeSalary employeeSalary)
    {
        await _efDbContext.EmployeeSalaries.AddAsync(employeeSalary);
        await _efDbContext.SaveChangesAsync();
        return employeeSalary.Id;
    }
    
    public async Task<IEnumerable<EmployeeSalary>> GetEmployeeInRangeAsync(int id, DateTime startDate, DateTime endDate)
    {
        string sql = "SELECT * FROM EmployeeSalaries WHERE EmployeeId = @Id AND SalaryDate >= @StartDate AND SalaryDate <= @EndDate";
        var employeeSalaries = await _dapperDbContext.CreateConnection().QueryAsync<EmployeeSalary>(sql, new { Id = id, StartDate = startDate, EndDate = endDate });
        if (employeeSalaries is null)
        {
            return Enumerable.Empty<EmployeeSalary>();
        }
        return employeeSalaries;
    }
}