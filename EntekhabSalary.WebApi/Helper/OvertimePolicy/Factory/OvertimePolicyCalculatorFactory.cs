namespace EntekhabSalary.WebApi.Helper.OvertimePolicy;

public class OvertimePolicyCalculatorFactory : IOvertimePolicyCalculatorFactory
{
    public IOvertimeCostCalculator CreateCalculator(string methodName, decimal basicSalary, decimal allowance)
    {
        return methodName switch
        {
            "CalculatorA" => new OvertimeCostCalculatorA(basicSalary, allowance),
            "CalculatorB" => new OvertimeCostCalculatorB(basicSalary, allowance),
            "CalculatorC" => new OvertimeCostCalculatorC(basicSalary, allowance),
            _ => throw new ArgumentException("Invalid method name!")
        };
    }
}