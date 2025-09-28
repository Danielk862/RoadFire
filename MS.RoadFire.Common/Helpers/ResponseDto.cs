using MS.RoadFire.Common.Resource;
using System.Net;

namespace MS.RoadFire.Common.Helpers
{
    public class ResponseDto<T>
    {
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        public string? Messages { get; set; } = MessagesResource.Success;

        public T? Data { get; set; }
    }
}
