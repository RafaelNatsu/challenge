namespace App.Contracts
{
    public class Request<T>
    {
        public Request() {}

        public Request(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }
}