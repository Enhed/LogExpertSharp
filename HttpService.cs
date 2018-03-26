using System;

namespace LogExpertSharp
{
    public abstract class HttpService : IDisposable
    {
        protected readonly Connection Connection;

        public HttpService(Connection connection)
        {
            Connection = connection;
        }

        public HttpService(string token)
        {
            Connection = new Connection(token);
        }

        public void Dispose() => Connection.Dispose();
    }
}