using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Http
{
    public class HttpRequest
    {
        public HttpRequest(HttpMethod method, string url, HttpHeaderCollection headers, string body = null )
        {
            Guard.AgainstNull(method, "Http Request Method");
            Guard.AgainstNull(url, "Http Request Url");
            Guard.AgainstNull(headers, "Http Request Headers");

            this.Method = method;
            this.Url = url;
            this.Headers = headers;
            this.Body = body;
        }

        public HttpRequest(string method, string url, string[] headers, string body = null)
        {
            Guard.AgainstNull(method, "Http Request Method");
            Guard.AgainstNull(url, "Http Request Url");
            Guard.AgainstNull(headers, "Http Request Headers");

            this.Method = HttpRequest.ParseMethod(method);
            this.Url = url;
            this.Headers = HttpHeaderCollection.Parse(headers);
            this.Body = body;

        }
        public HttpMethod Method { get; private set; }
        public string Url { get; private set; }
        public HttpHeaderCollection Headers { get; private set; } = new HttpHeaderCollection();
        public string Body { get; private set; }

        public static HttpRequest Parse(string requestString)
        {
            var requestLines = requestString.Split(GlobalConstants.NewLine);
            var firstLine = requestLines[0];
            var firstLineMembers = firstLine.Split(' ');

            var method = ParseMethod(firstLineMembers[0]);
            var url = firstLineMembers[1];

            var headerAndBodyLines = requestLines.Skip(1);
            var headerCollection = HttpHeaderCollection.Parse(headerAndBodyLines);

            var bodyLines = headerAndBodyLines.Skip(1 + headerCollection.Count);
            var body = string.Join(String.Empty, bodyLines);

            return new HttpRequest(method, url, headerCollection, body);
        }

        private static HttpMethod ParseMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"{method} is not supported.")
            };
        }

        public override string ToString()
        {
            var requestBuilder = new StringBuilder();
            requestBuilder.AppendLine($"{this.Method} {this.Url} HTTP/1.1");
            foreach (var header in this.Headers)
            {
                requestBuilder.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(this.Body))
            {
                requestBuilder.AppendLine();
                requestBuilder.AppendLine(this.Body);
            }

            return requestBuilder.ToString();
        }
    }
}
