using Newtonsoft.Json;

namespace LogExpertSharp.Accounting
{
    public sealed class User
    {
        public int Id;

        [JsonProperty("UserName")]
        public string Name;
    }
}