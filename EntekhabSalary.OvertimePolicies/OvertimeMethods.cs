namespace EntekhabSalary.SalaryPolicies;

public class OvertimeMethods
{
    public decimal BasicSalary { get; set; }
    public decimal Allowance { get; set; }

    public OvertimeMethods(decimal basicSalary, decimal allowance) => (BasicSalary, Allowance) = (basicSalary, allowance);

    public decimal CalculateA()
    {
        return BasicSalary / 2 + Allowance / 4;
    }

    public decimal CalculateB()
    {
        return BasicSalary / 3 + (decimal) Math.Sqrt(Convert.ToDouble(Allowance));   
    }
    
    public decimal CalculateC()
    {
        return BasicSalary / Allowance;
    }
}