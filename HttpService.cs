namespace LogExpertSharp
{
    public abstract class HttpService
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
    }
}