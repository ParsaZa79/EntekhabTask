namespace EntekhabSalary.WebApi.Helper.Deserializer;

public abstract class BaseDataDeserializer
{
    protected string Data { get; set; }

    protected BaseDataDeserializer(string data) => Data = data;

}