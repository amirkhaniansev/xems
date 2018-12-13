namespace ExamsApiConsumer.Core
{
    public class Response<TData>
    {
        public TData Data { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public Response(TData data, ResponseStatus responseStatus)
        {
            this.Data = data;
            this.ResponseStatus = responseStatus;
        }
    }
}