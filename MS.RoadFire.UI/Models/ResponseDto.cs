namespace MS.RoadFire.UI.Models
{
    public class ResponseDto<T>
    {
        public int Code { get; set; }
        public string? Messages { get; set; }
        public T? Data { get; set; }
    }
}