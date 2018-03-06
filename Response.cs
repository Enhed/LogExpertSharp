namespace LogExpertSharp
{
    public class Response
    {
        public bool Success;
        public string Message;
    }

    public abstract class ResponseData<TData> : Response
    {
        public TData Data;
    }
}