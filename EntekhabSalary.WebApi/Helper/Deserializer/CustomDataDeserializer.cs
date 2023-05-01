using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Helper.Extensions;
using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Helper.Deserializer;

public class CustomDataDeserializer : BaseDataDeserializer, IBaseDataDeserializer
{
    public CustomDataDeserializer(string data) : base(data)
    {
    }

    public EmployeeData? Deserialize()
    {
        var splitData = Data.Split("/", StringSplitOptions.RemoveEmptyEntries);

        var employeeData = new EmployeeData(
            splitData[0],
            splitData[1],
            decimal.Parse(splitData[2]),
            decimal.Parse(splitData[3]),
            decimal.Parse(splitData[4]),
            splitData[5].ToGregorianDate());

        return employeeData;

    }
}