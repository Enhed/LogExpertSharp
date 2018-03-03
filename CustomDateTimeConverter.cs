using Newtonsoft.Json.Converters;

namespace ConsoleFoo
{
    public sealed class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
        }
    }

}