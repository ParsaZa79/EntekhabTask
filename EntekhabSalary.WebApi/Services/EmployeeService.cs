using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Helper.Deserializer.Factory;
using EntekhabSalary.WebApi.Helper.Extensions;
using EntekhabSalary.WebApi.Helper.OvertimePolicy;
using EntekhabSalary.WebApi.Helper.SalaryCalculator;
using EntekhabSalary.WebApi.Models;
using EntekhabSalary.WebApi.Repositories.Abstract;
using Mapster;

namespace EntekhabSalary.WebApi.Services;

public class EmployeeService : IEmployeeService
{

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeSalaryRepository _employeeSalaryRepository;
    
    private readonly IDataSerializerFactory _dataSerializerFactory;
    private readonly IOvertimePolicyCalculatorFactory _overtimePolicyCalculator;
    private readonly ISalaryCalculator _salaryCalculator;
    
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IEmployeeRepository employeeRepository,
        IEmployeeSalaryRepository employeeSalaryRepository,
        IDataSerializerFactory dataSerializerFactory,
        IOvertimePolicyCalculatorFactory overtimePolicyCalculator,
        ILogger<EmployeeService> logger,
        ISalaryCalculator salaryCalculator)
    {
        _employeeRepository = employeeRepository;
        _employeeSalaryRepository = employeeSalaryRepository;
        _dataSerializerFactory = dataSerializerFactory;
        _overtimePolicyCalculator = overtimePolicyCalculator;
        _logger = logger;
        _salaryCalculator = salaryCalculator;
    }


    public async Task<int?> AddEmployee(AddEmployeeRequest addEmployeeRequest, string dataType)
    {
        #region Map employee data to employee entity

        var data = addEmployeeRequest.Data;
        var dataTypeEnum = dataType.ToDataType();
        var dataSerializer = _dataSerializerFactory.CreateSerializer(dataTypeEnum, data);
        var employeeData = dataSerializer.Deserialize();

        var employee = employeeData!.Adapt<Employee>();

        var employeeId = await _employeeRepository.AddAsync(employee);
        
        #endregion

        #region Map employee data to employee-salary entity

        var overtimeCostCalculator = _overtimePolicyCalculator.CreateCalculator(addEmployeeRequest.OverTimeCalculator,
            employeeData!.BasicSalary,
            employeeData.Allowance);

        var overtimeCost = overtimeCostCalculator.Calculate();
        var totalSalary = _salaryCalculator.Calculate(employeeData.BasicSalary, employeeData.Allowance,
            employeeData.Transportation, overtimeCost);


        if (employeeId is not null)
        {
            var employeeSalary = employeeData.Adapt<EmployeeSalary>();
            employeeSalary.TotalSalary = totalSalary;
            employeeSalary.EmployeeId = (int)employeeId;

            await _employeeSalaryRepository.AddAsync(employeeSalary);
        }

        #endregion
        
        return employeeId;
    }

    public async Task<bool> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
    {
        return await _employeeRepository.UpdateAsync(updateEmployeeRequest.Employee);
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }

    public async Task<Employee?> GetEmployee(int id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }
}