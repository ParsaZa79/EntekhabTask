namespace EntekhabSalary.WebApi.Helper.OvertimePolicy;

public class OvertimeCostCalculatorB : BaseOvertimeCostCalculator, IOvertimeCostCalculator
{
    public OvertimeCostCalculatorB(decimal basicSalary, decimal allowance) : base(basicSalary, allowance)
    {
    }

    public decimal Calculate() => OvertimeMethods.CalculateB();
}