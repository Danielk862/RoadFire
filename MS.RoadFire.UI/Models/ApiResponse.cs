namespace MS.RoadFire.UI.Models
{
    public class ApiResponse
    {
        public int code { get; set; }
        public string messages { get; set; } = string.Empty;
        public object data { get; set; } = string.Empty;
    }
}
