namespace EntekhabSalary.WebApi.Helper.SalaryCalculator;

public class DefaultSalaryCalculator : ISalaryCalculator
{
    public decimal Calculate(decimal basicSalary, decimal allowance, decimal transportation, decimal overtimeCost)
    {
        return basicSalary + allowance + transportation - overtimeCost;
    }
}
