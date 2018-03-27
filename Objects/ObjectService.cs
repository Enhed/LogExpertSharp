using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LogExpertSharp.Extensions;

namespace LogExpertSharp.Objects
{
    public sealed class ObjectService : HttpService
    {
        private const string NAME = "Object";

        public ObjectService(Connection connection) : base(connection)
        {
        }

        public ObjectService(string token) : base(token)
        {
        }

        public async Task<ObjectPositionInfo> GetObjectPosition(int objectId, bool withGeofence = true)
        {
            const string addr = "address";
            const string geo = "geofence";

            var resultType = withGeofence ? geo : addr;

            var id = ( nameof(objectId), objectId.ToString() );
            var rt = ( nameof(resultType), resultType );

            var method = $"{NAME}/{nameof(GetObjectPosition)}";
            var task = Connection.Post(method, id, rt);

            using(var response = (await task))
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ObjectPositionInfo>(result);
            }
        }
    }

    public sealed class ObjectPositionInfo
    {
        public bool Success;
        public string Message;
        public string Position;
        public DateTime PositionTime;
    }
}