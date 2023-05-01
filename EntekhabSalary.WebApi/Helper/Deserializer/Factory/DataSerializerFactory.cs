using EntekhabSalary.WebApi.Helper.Enums;

namespace EntekhabSalary.WebApi.Helper.Deserializer.Factory;

public class DataSerializerFactory : IDataSerializerFactory
{
    public IBaseDataDeserializer CreateSerializer(DataType dataType, string data)
    {
        return dataType switch
        {
            DataType.Json => new JsonDataDeserializer(data),
            DataType.Xml => new XmlDataDeserializer(data),
            DataType.Cs => new CsDataDeserializer(data),
            DataType.Custom => new CustomDataDeserializer(data),
            _ => throw new ArgumentException("Invalid data type!", nameof(data))
        };
    }
}
