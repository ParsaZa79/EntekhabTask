using System.Xml.Serialization;
using EntekhabSalary.WebApi.Contracts.Requests;
using EntekhabSalary.WebApi.Models;

namespace EntekhabSalary.WebApi.Helper.Deserializer;

public class XmlDataDeserializer : BaseDataDeserializer, IBaseDataDeserializer
{
    public XmlDataDeserializer(string data) : base(data)
    {
    }

    public EmployeeData? Deserialize()
    {
        var xmlSerializer = new XmlSerializer(typeof(EmployeeData));
        using var stringReader = new StringReader(Data);
        var employee = (EmployeeData)xmlSerializer.Deserialize(stringReader)!;

        return employee;
    }
}