namespace EntekhabSalary.WebApi.Helper.SalaryCalculator;

public interface ISalaryCalculator
{
    decimal Calculate(decimal basicSalary, decimal allowance, decimal transportation, decimal overtimeCost);
}
