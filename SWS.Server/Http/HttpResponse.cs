using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Http
{
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers.Add("Server", "SoftUni Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow.ToString("r")}");

        }
        public HttpStatusCode StatusCode { get; init; }
        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();
        public string Content { get; init; }
    }
}
