using EntekhabSalary.WebApi.Models;
using EntekhabSalary.WebApi.Repositories.Abstract;

namespace EntekhabSalary.WebApi.Services;

public class EmployeeSalaryService : IEmployeeSalaryService
{

    private readonly IEmployeeSalaryRepository _employeeSalaryRepository;

    public EmployeeSalaryService(IEmployeeSalaryRepository employeeSalaryRepository)
    {
        _employeeSalaryRepository = employeeSalaryRepository;
    }

    public async Task<IEnumerable<EmployeeSalary>> GetEmployeeSalariesInRangeAsync(int id, DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException($"{nameof(startDate)} should always be less than {nameof(endDate)}!");
        }
        return await _employeeSalaryRepository.GetEmployeeInRangeAsync(id, startDate, endDate);
    }
}