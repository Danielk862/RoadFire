using System.Net;

namespace MS.RoadFire.UI.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error) return null;

            return HttpResponseMessage.StatusCode switch
            {
                HttpStatusCode.NotFound => "Recurso no encontrado.",
                HttpStatusCode.BadRequest => await HttpResponseMessage.Content.ReadAsStringAsync(),
                HttpStatusCode.Unauthorized => "Tienes que estar logueado para ejecutar esta operación.",
                HttpStatusCode.Forbidden => "No tienes permisos para hacer esta operación.",
                _ => "Ha ocurrido un error inesperado."
            };
        }
    }
}