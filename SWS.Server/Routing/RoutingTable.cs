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

        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(path, "Path");
            Guard.AgainstNull(method, "Http Method");
            Guard.AgainstNull(response, "Http Response in router");

            this.routes[method][path] = response;

            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
                        => this.Map(HttpMethod.Get, path, response);
        public IRoutingTable MapPost(string path, HttpResponse response)
                        => this.Map(HttpMethod.Post, path, response);
        public IRoutingTable MapPut(string path, HttpResponse response)
                        => this.Map(HttpMethod.Put, path, response);
        public IRoutingTable MapDelete(string path, HttpResponse response)
                        => this.Map(HttpMethod.Delete, path, response);

        public HttpResponse MatchRequest(HttpRequest request)
        {
            if (!this.routes.ContainsKey(request.Method))
            {
                return new BadRequestResponse(@$"<h1>{request.Method} is not a supported HttpMethod</h1>");
            }

            if (!this.routes[request.Method].ContainsKey(request.Path))
            {
                return new NotFoundResponse(@$"<h1>{request.Path} does not represent a valid Path</h1>");
            }

            return this.routes[request.Method][request.Path];
        }
    }
}
