using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EntekhabSalary.WebApi.Helper.Deserializer;

public class JsonDataDeserializer : BaseDataDeserializer, IBaseDataDeserializer
{
    public JsonDataDeserializer(string data) : base(data)
    {
    }
    
    public EmployeeData? Deserialize() => 
        JsonSerializer.Deserialize<EmployeeData>(Data);
    
}
