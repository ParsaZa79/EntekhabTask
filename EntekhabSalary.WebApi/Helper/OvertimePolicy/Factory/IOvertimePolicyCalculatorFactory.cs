namespace EntekhabSalary.WebApi.Helper.OvertimePolicy;

public interface IOvertimePolicyCalculatorFactory
{
    IOvertimeCostCalculator CreateCalculator(string methodName, decimal basicSalary, decimal allowance);
}