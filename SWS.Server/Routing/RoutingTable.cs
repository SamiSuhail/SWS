using Common;
using SWS.Server.Http;
using SWS.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;
        public RoutingTable()
        {
            this.routes = new()
            {
                [HttpMethod.Get] = new(),
                [HttpMethod.Post] = new(),
                [HttpMethod.Put] = new(),
                [HttpMethod.Delete] = new(),
            };
        }

        public IRoutingTable Map(string url, HttpMethod method, HttpResponse response)
        {
            Guard.AgainstNull(url, "Url");
            Guard.AgainstNull(method, "Http Method");
            Guard.AgainstNull(response, "Http Response in router");

            return method switch
            {
                HttpMethod.Get => this.MapGet(url, response),
                //HttpMethod.Post => this.routes[HttpMethod.Post][url] = response,
                //HttpMethod.Put => this.routes[HttpMethod.Put][url] = response,
                //HttpMethod.Delete => this.routes[HttpMethod.Delete][url] = response,
                _ => throw new InvalidOperationException($"{method} is not supported")
            };
        }

        public IRoutingTable MapGet(string url, HttpResponse response)
        {
            Guard.AgainstNull(url, "Url");
            Guard.AgainstNull(response, "Http Response in router");

            this.routes[HttpMethod.Get][url] = response;

            return this;
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            if (!this.routes.ContainsKey(request.Method))
            {
                return new BadRequestResponse(@$"<h1>{request.Method} is not a supported HttpMethod</h1>");
            }

            if (!this.routes[request.Method].ContainsKey(request.Url))
            {
                return new NotFoundResponse(@$"<h1>{request.Url} does not represent a valid Url</h1>");
            }

            return this.routes[request.Method][request.Url];
        }
    }
}
