using System.Globalization;
using AutoMapper;

namespace UniHelp.Features.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
    }
    
    private static DateTime? ParseDateTime(string dateString)
    {
        return DateTime.TryParseExact(
            dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)
            ? result
            : null;
    }
}