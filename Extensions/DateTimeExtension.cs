using System;

namespace LogExpertSharp.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToRestString(this DateTime date)
        {
            const string format = "dd/MM/yyyy HH:mm:ss.fff";
            return date.ToString(format);
        }
    }
}