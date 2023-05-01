using EntekhabSalary.SalaryPolicies;

namespace EntekhabSalary.WebApi.Helper.OvertimePolicy;

public class OvertimeCostCalculatorA : BaseOvertimeCostCalculator, IOvertimeCostCalculator
{
    public OvertimeCostCalculatorA(decimal basicSalary, decimal allowance) : base(basicSalary, allowance)
    {
    }

    public decimal Calculate() => OvertimeMethods.CalculateA();
}