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

            return new HttpRequest() { Method = method, Url = url, Headers = headerCollection, Body = body };
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

        
    }
}
