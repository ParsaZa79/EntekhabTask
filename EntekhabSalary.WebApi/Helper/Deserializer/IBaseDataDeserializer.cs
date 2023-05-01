using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Helper.Deserializer;

public interface IBaseDataDeserializer
{
    EmployeeData? Deserialize();
}