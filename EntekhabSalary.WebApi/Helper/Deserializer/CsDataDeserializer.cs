using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Helper.Deserializer;

public class CsDataDeserializer : BaseDataDeserializer, IBaseDataDeserializer
{
    public CsDataDeserializer(string data) : base(data)
    {
    }

    public EmployeeData? Deserialize()
    {
        throw new NotImplementedException();
    }
}