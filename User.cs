using Newtonsoft.Json;

namespace LogExpertSharp
{
    public sealed class User
    {
        public int Id;

        [JsonProperty("UserName")]
        public string Name;
    }
}