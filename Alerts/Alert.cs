using System;
using Newtonsoft.Json;

namespace LogExpertSharp.Alerts
{
    public sealed class Alert
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("text")]        
        public string Text;

        [JsonProperty("objectId")]
        public int ObjectId;

        [JsonProperty("objectName")]
        public string ObjectName;

        [JsonProperty("objectType")]
        public string ObjectType;

        [JsonProperty("creationDate")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime CreationDate;
    }

}