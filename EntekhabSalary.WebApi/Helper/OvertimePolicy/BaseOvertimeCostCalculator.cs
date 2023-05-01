using EntekhabSalary.SalaryPolicies;

namespace EntekhabSalary.WebApi.Helper.OvertimePolicy;

public abstract class BaseOvertimeCostCalculator
{
    protected decimal BasicSalary { get; set; }
    protected decimal Allowance { get; set; }
    protected OvertimeMethods OvertimeMethods { get; set; }

    protected BaseOvertimeCostCalculator(decimal basicSalary, decimal allowance)
    {
        BasicSalary = basicSalary;
        Allowance = allowance;
        OvertimeMethods = new OvertimeMethods(BasicSalary, Allowance);
    }
}