using System.Globalization;

namespace EntekhabSalary.WebApi.Helper.Extensions;

public static class DatetimeExtensions
{
    public static DateTime ToPersianDatetime(this DateTime gregorianDateTime)
    {
        var persianCalendar = new PersianCalendar();

        var persianDatetime = persianCalendar.ToDateTime(gregorianDateTime.Year, gregorianDateTime.Month,
            gregorianDateTime.Day, 0, 0, 0, PersianCalendar.PersianEra);

        return persianDatetime;
    }
    
    public static DateTime ToGregorianDate(this string jalaliDate)
    {
        if (jalaliDate is not { Length: 8 })
        {
            throw new ArgumentException("Invalid date format.");
        }

        var jalaliYear = int.Parse(jalaliDate.Substring(0, 4));
        var jalaliMonth = int.Parse(jalaliDate.Substring(4, 2));
        var jalaliDay = int.Parse(jalaliDate.Substring(6, 2));

        var persianCalendar = new PersianCalendar();
        var gregorianDate = persianCalendar.ToDateTime(jalaliYear, jalaliMonth, jalaliDay, 0, 0, 0, 0);
        return gregorianDate;
    }
    
    public static string ToFormattedDate(this string unformattedDate)
    {
        if (unformattedDate is not { Length: 8 })
        {
            throw new ArgumentException("Invalid date format.");
        }
        
        var year = unformattedDate.Substring(0, 4);
        var month = unformattedDate.Substring(4, 2);
        var day = unformattedDate.Substring(6, 2);
        return $"{year}-{month}-{day}";
    }
}