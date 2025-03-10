using SGH.Application.Interfaces;

namespace SGH.Application.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrent() => DateTime.Now;
}