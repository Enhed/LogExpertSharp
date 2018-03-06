using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public async Task SetViewed(params Alert[] alerts)
        {
            if(alerts?.Length == 0) return;
            const string paramName = "ids[]";

            var ids = alerts.Select(a => (paramName, a.Id.ToString())).ToArray();

            using(var response = await Connection.Post($"{NAME}/{nameof(SetViewed)}", ids))
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
            //date format: 28/02/2018 00:00:00.000
            var format = "dd/MM/yyyy HH:mm:ss.fff";

            var start = ( nameof(startDate), startDate.ToString(format));
            var end = ( nameof(endDate), endDate.ToString(format) );

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
    }

}