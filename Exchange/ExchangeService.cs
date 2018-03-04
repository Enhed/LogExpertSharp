using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogExpertSharp.Exchange
{
    public sealed class ExchangeService : HttpService
    {
        public ExchangeService(Connection connection) : base(connection)
        {
        }

        public ExchangeService(string token) : base(token)
        {
        }

        public async Task<TimeSpan> GetEngineHours(int objectId, DateTime begin, DateTime end)
        {
            var format = "dd/MM/yyyy HH:mm:ss.fff";

            var i = ( nameof(objectId) , objectId.ToString() );
            var b = ( nameof(begin), begin.ToString(format) );
            var e = ( nameof(end), end.ToString(format) );

            var method = $"{nameof(ExchangeService)}/{nameof(GetEngineHours)}";
            var task = Connection.Post(method, i, b, e);

            using(var response = await task)
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                return long.TryParse( result, out var seconds ) ? TimeSpan.FromSeconds(seconds)
                    : throw new Exception($"incorrect server value: {result}");
            }
        }

        public async Task<float> GetMileage(int objectId, 
            DateTime begin, DateTime end, bool useRideDetector = false, bool useAltitude = true)
        {
            var format = "dd/MM/yyyy HH:mm:ss.fff";

            var i = ( nameof(objectId) , objectId.ToString() );
            var b = ( nameof(begin), begin.ToString(format) );
            var e = ( nameof(end), end.ToString(format) );
            var rd = ( nameof(useRideDetector), useRideDetector.ToString() );
            var alt = ( nameof(useAltitude), useAltitude.ToString() );

            var method = $"{nameof(ExchangeService)}/{nameof(GetMileage)}";
            var task = Connection.Post(method, i, b, e, rd, alt);

            using(var response = ( await task ).EnsureSuccessStatusCode() )
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MileageItem[]>(result)
                    .Select(x => x.Length).Sum();
            }
        }

        private sealed class MileageItem
        {
            public float Length;
        }
    }
}