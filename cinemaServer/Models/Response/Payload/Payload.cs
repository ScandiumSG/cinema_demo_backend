namespace cinemaServer.Models.Response.Payload
{
    public class Payload<T> where T : class
    {
        public string Status { get; set; } = "success";

        public DateTime ResponseTime { get; set; }

        public T Data { get; set; }

        public Payload(T data) 
        {
            ResponseTime = DateTime.Now;
            Data = data;
        }

    }
}
