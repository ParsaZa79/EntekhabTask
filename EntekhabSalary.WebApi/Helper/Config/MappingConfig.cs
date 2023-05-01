using EntekhabSalary.WebApi.Contracts.Requests;
using Mapster;
using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Helper.Config;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<EmployeeData, Employee>.NewConfig()
            .Map(dest => dest.Id, src => 0)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.BasicSalary, src => src.BasicSalary)
            .Map(dest => dest.Allowance, src => src.Allowance)
            .Map(dest => dest.Transportation, src => src.Transportation);

        TypeAdapterConfig<EmployeeData, EmployeeSalary>.NewConfig()
            .Map(dest => dest.Id, src => 0)
            .Map(dest => dest.SalaryDate, src => src.Date)
            .Ignore(dest => dest.EmployeeId)
            .Ignore(dest => dest.Employee);
    }
}
