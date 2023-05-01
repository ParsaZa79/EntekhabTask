namespace EntekhabSalary.WebApi.Helper.OvertimePolicy;

public class OvertimeCostCalculatorC : BaseOvertimeCostCalculator, IOvertimeCostCalculator
{
    public OvertimeCostCalculatorC(decimal basicSalary, decimal allowance) : base(basicSalary, allowance)
    {
    }

    public decimal Calculate() => OvertimeMethods.CalculateC();
}