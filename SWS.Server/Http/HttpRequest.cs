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
        public HttpRequest(HttpMethod method, string url, HttpHeaderCollection headers, string body = null)
        {
            Guard.AgainstNull(method, "Http Request Method");
            Guard.AgainstNull(url, "Http Request Path");
            Guard.AgainstNull(headers, "Http Request Headers");

            this.Method = method;
            (var path, var queryString) = ParseUrl(url);
            this.Path = path;
            this.Query = queryString;
            this.Headers = headers;
            this.Body = body;
        }

        public HttpRequest(string method, string url, string[] headers, string body = null)
        {
            Guard.AgainstNull(method, "Http Request Method");
            Guard.AgainstNull(url, "Http Request Path");
            Guard.AgainstNull(headers, "Http Request Headers");

            this.Method = HttpRequest.ParseMethod(method);
            (var path, var queryString) = ParseUrl(url);
            this.Path = path;
            this.Query = queryString;
            this.Headers = HttpHeaderCollection.Parse(headers);
            this.Body = body;

        }

        public HttpMethod Method { get; private set; }
        public string Path { get; private set; }
        public Dictionary<string,string> Query { get; private set; }
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

        private static Dictionary<string,string> ParseQuery(string queryString)
        {
            Guard.AgainstNull(queryString, "Query String");
            var queries = queryString.Split('&');

            var dict = new Dictionary<string, string>();

            foreach (var query in queries)
            {
                var queryParts = query.Split('=');

                if (queryParts.Length != 2)
                {
                    continue;
                }

                var queryKey = queryParts[0];
                var queryValue = queryParts[1];

                dict.Add(queryKey, queryValue);
            }

            return dict;
        }

        private static (string path, Dictionary<string,string> query) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);
            var path = urlParts[0];
            Dictionary<string, string> query = null;

            if (urlParts.Length > 1)
            {
                query = ParseQuery(urlParts[1]);
            }

            return (path, query);
        }

        public override string ToString()
        {
            var requestBuilder = new StringBuilder();
            requestBuilder.AppendLine($"{this.Method} {this.Path}{QueryToString(this.Query)} HTTP/1.1");
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

        private static string QueryToString(Dictionary<string, string> query)
        {
            if (query == null)
            {
                return null;
            }
            var queryBuilder = new List<string>();

            foreach (var kvp in query)
            {
                queryBuilder.Add($"{kvp.Key}={kvp.Value}");
            }

            if (!query.Any())
            {
                return null;
            }

            return '?' + string.Join('&', queryBuilder);
        }
    }
}
