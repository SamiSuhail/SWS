using Common;
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
            Guard.AgainstNull(statusCode, "Http Response Status Code");
            this.StatusCode = statusCode;
            this.Headers.Add("Server", "SoftUni Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow.ToString("r")}");

        }
        public HttpStatusCode StatusCode { get; init; }
        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();
        public string Content { get; init; }

        public override string ToString()
        {
            var responseBuilder = new StringBuilder();

            responseBuilder.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");
            foreach (var header in Headers)
            {
                responseBuilder.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(this.Content))
            {
                responseBuilder.AppendLine();
                responseBuilder.AppendLine(this.Content);
            }

            return responseBuilder.ToString();
        }
    }
}
