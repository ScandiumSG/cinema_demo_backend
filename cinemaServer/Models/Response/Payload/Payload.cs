using System.Text.Json.Serialization;

namespace cinemaServer.Models.Response.Payload
{
    public class Payload<T> where T : class
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "success";

        [JsonPropertyName("responseTime")]
        public DateTime ResponseTime { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        public Payload(T data) 
        {
            ResponseTime = DateTime.Now;
            Data = data;
        }

    }
}
