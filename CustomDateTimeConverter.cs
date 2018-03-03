using Newtonsoft.Json.Converters;

namespace LogExpertSharp
{
    public sealed class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
        }
    }

}