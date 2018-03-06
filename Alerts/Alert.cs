using System;
using Newtonsoft.Json;

/*

"Id": 146810,
"TriggerId": 63,
"ObjectId": 568,
"CreationDate": "2018-03-05T15:50:00.68",
"MessageText": "Объект МАЗ О214МК потерял связь или координаты!",
"SentTo": "Не указан адресат",
"AlertsViews": [],
"TriggerType": "Потеря координат"

 */

namespace LogExpertSharp.Alerts
{
    public sealed class Alert
    {
        public int Id;

        [JsonProperty("MessageText")]        
        public string Message;
        public string SentTo;
        public string TriggerType;
        public int ObjectId;

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime CreationDate;
    }

}