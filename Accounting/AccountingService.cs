using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogExpertSharp.Accounting
{

    public sealed class AccountingService : HttpService
    {
        public AccountingService(Connection connection) : base(connection)
        {
        }

        public AccountingService(string token) : base(token)
        {
        }

        public async Task<User[]> GetUsers()
        {
            var method = $"{nameof(Accounting)}/{nameof(GetUsers)}";

            using(var response = await Connection.Post(method))
            {
                response.EnsureSuccessStatusCode();
                var userResponse = JsonConvert.DeserializeObject<UsersResponse>(await response.Content.ReadAsStringAsync());
                return userResponse.Success ? userResponse.Users : throw new Exception(userResponse.Message);
            }
        }
    }
}