using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LogExpertSharp.Extensions;

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
            var i = ( nameof(objectId) , objectId.ToString() );
            var b = ( nameof(begin), begin.ToRestString() );
            var e = ( nameof(end), end.ToRestString() );

            var method = $"{nameof(Exchange)}/{nameof(GetEngineHours)}";
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
            var i = ( nameof(objectId) , objectId.ToString() );
            var b = ( nameof(begin), begin.ToRestString() );
            var e = ( nameof(end), end.ToRestString() );
            var rd = ( nameof(useRideDetector), useRideDetector.ToString() );
            var alt = ( nameof(useAltitude), useAltitude.ToString() );

            var method = $"{nameof(Exchange)}/{nameof(GetMileage)}";
            var task = Connection.Post(method, i, b, e, rd, alt);

            using(var response = ( await task ).EnsureSuccessStatusCode() )
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MileageItem[]>(result)
                    .Select(x => x.Length).Sum();
            }
        }

        public Task<float> GetMileage(int objectId, 
            DateTime begin, bool useRideDetector = false, bool useAltitude = true)
        {
            return GetMileage(objectId, begin, DateTime.Now, useRideDetector, useAltitude);
        }

        public async Task<SplittedMileage> GetMileageSplitted(int objectId,
            DateTime begin, DateTime end, int speedOfSplitting = 60, bool useRideDetector = false, bool useAltitude = true)
        {
            var i = ( nameof(objectId) , objectId.ToString() );
            var b = ( nameof(begin), begin.ToRestString() );
            var e = ( nameof(end), end.ToRestString() );
            var rd = ( nameof(useRideDetector), useRideDetector.ToString() );
            var alt = ( nameof(useAltitude), useAltitude.ToString() );
            var speed = ( nameof(speedOfSplitting), speedOfSplitting.ToString() );

            var method = $"{nameof(Exchange)}/{nameof(GetMileageSplitted)}";
            var task = Connection.Post(method, i, b, e, rd, alt, speed);

            using(var response = ( await task ).EnsureSuccessStatusCode() )
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SplittedMileage>(result);
            }
        }

        public Task<SplittedMileage> GetMileageSplitted(int objectId,
            DateTime begin, int speedOfSplitting = 60, bool useRideDetector = false, bool useAltitude = true)
        {
            return GetMileageSplitted(objectId, begin, DateTime.Now, speedOfSplitting, useRideDetector, useAltitude);
        }

        private sealed class MileageItem
        {
            public float Length;
        }
    }
}