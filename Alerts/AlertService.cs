using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LogExpertSharp.Extensions;

namespace LogExpertSharp.Alerts
{
    public sealed class AlertService : HttpService
    {
        private const string NAME = "Alert";

        public AlertService(Connection connection) : base(connection)
        {
        }

        public AlertService(string token) : base(token)
        {
        }

        public async Task<Alert[]> GetUnreaded()
        {
            using(var response = await Connection.Post($"{NAME}/{nameof(GetUnreaded)}"))
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                var alertResponse = JsonConvert.DeserializeObject<AlertResponse>(result);
                return alertResponse.Success ? alertResponse.Data : throw new Exception(alertResponse.Message);
            }
        }

        public Task SetViewed(params Alert[] alerts)
            => SetViewed(alerts.Select(a => a.Id).ToArray());

        public async Task SetViewed(int[] ids)
        {
            if(ids?.Length == 0) return;
            var paramName = $"{nameof(ids)}[]";
            var tuples = ids.Select(i => (paramName, i.ToString())).ToArray();

            using(var response = await Connection.Post($"{NAME}/{nameof(SetViewed)}", tuples))
            {
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response>(stringResult);

                if(!result.Success) throw new Exception(result.Message ?? "invalid operation");
            }
        }

        public async Task ViewAll()
        {
            using(var response = await Connection.Post($"{NAME}/{nameof(ViewAll)}"))
            {
                response.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<Response>( await response.Content.ReadAsStringAsync() );
                if(!result.Success) throw new Exception(result.Message ?? "invalid operation");
            }
        }

        public async Task<Alert[]> GetAlertsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var start = ( nameof(startDate), startDate.ToRestString());
            var end = ( nameof(endDate), endDate.ToRestString() );

            var method = $"{NAME}/{nameof(GetAlertsBetweenDates)}";
            var task = Connection.Post(method, start, end);

            using(var response = ( await task ).EnsureSuccessStatusCode() )
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert
                    .DeserializeObject<AlertResponse>(stringResult);

                return result.Success ? result.Data : throw new Exception(result.Message);
            }
        }

        public async Task<Trigger[]> GetTriggers()
        {
            var method = $"{NAME}/{nameof(GetTriggers)}";

            using(var response = ( await Connection.Post(method) ).EnsureSuccessStatusCode() )
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Trigger[]>(result);
            }
        }

        public async Task<Alert[]> GetAlerts(int triggerId, DateTime begin, DateTime end)
        {
            var method = $"{NAME}/{nameof(GetAlerts)}";

            var id = ( nameof(triggerId), triggerId.ToString() );
            var b = ( nameof(begin), begin.ToRestString() );
            var e = ( nameof(end), end.ToRestString() );

            using(var response = (await Connection.Post(method, id, b, e)).EnsureSuccessStatusCode() )
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Alert[]>(result);
            }
        }

        public Task<Alert[]> GetAlerts(Trigger trigger, DateTime begin, DateTime end)
        {
            return GetAlerts(trigger.Id, begin, end);
        }

        public Task<Alert[]> GetAlerts(Trigger trigger, DateTime begin)
        {
            return GetAlerts(trigger.Id, begin, DateTime.Now);
        }
    }

    public sealed class Trigger
    {
        public int Id;
        public string Name;
        public string Type;
        public bool IsEnabled;
        public object Settings;
    }
}