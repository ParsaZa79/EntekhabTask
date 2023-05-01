using EntekhabSalary.WebApi.Helper.Enums;

namespace EntekhabSalary.WebApi.Helper.Deserializer.Factory;

public interface IDataSerializerFactory
{
    IBaseDataDeserializer CreateSerializer(DataType dataType, string data);
}
