using Dapper;
using EntekhabSalary.WebApi.Data;
using EntekhabSalary.WebApi.Models;
using EntekhabSalary.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EntekhabSalary.WebApi.Repositories.Concrete;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EfDbContext _efDbContext;
    private readonly DapperDbContext _dapperDbContext;

    public EmployeeRepository(EfDbContext efDbContext, DapperDbContext dapperDbContext)
    {
        _efDbContext = efDbContext;
        _dapperDbContext = dapperDbContext;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _dapperDbContext.CreateConnection().QuerySingleAsync<Employee>("SELECT * FROM Employees WHERE Id = @Id;", new {Id = id});
    }

    public async Task<int?> AddAsync(Employee entity)
    {
        _efDbContext.Employees.Add(entity);
        await _efDbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Employee entity)
    {
        _efDbContext.Employees.Update(entity);
        _efDbContext.Entry(entity).State = EntityState.Modified;
        var rowsChanged = await _efDbContext.SaveChangesAsync();
        return rowsChanged > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _efDbContext.Employees.FindAsync(id);
        if (employee != null)
        {
            _efDbContext.Employees.Remove(employee);
            var rowsChanged = await _efDbContext.SaveChangesAsync();
            return rowsChanged > 0;
        }

        return false;
    }
}
