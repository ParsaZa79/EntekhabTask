using EntekhabSalary.WebApi.Helper.Enums;

namespace EntekhabSalary.WebApi.Helper.Extensions;

public static class EnumExtensions
{
    public static DataType ToDataType(this string data) => data.ToLower() switch
    {
        "json" => DataType.Json,
        "xml" => DataType.Xml,
        "cs" => DataType.Cs,
        "custom" => DataType.Custom,
        _ => DataType.None
    };
}