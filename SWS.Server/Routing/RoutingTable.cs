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
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest,HttpResponse>>> routes;
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
            Guard.AgainstNull(response, "Http Response in router");


            return this.Map(method, path, request => response);
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunc)
        {
            Guard.AgainstNull(path, "Path");
            Guard.AgainstNull(method, "Http Method");
            Guard.AgainstNull(responseFunc, "Http ResponseFunc in router");

            this.routes[method][path] = responseFunc;

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

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunc)
                    => this.Map(HttpMethod.Get, path, responseFunc);
        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunc)
                    => this.Map(HttpMethod.Post, path, responseFunc);
        public IRoutingTable MapPut(string path, Func<HttpRequest, HttpResponse> responseFunc)
                    => this.Map(HttpMethod.Put, path, responseFunc);
        public IRoutingTable MapDelete(string path, Func<HttpRequest, HttpResponse> responseFunc)
                    => this.Map(HttpMethod.Delete, path, responseFunc);



        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            if (!this.routes.ContainsKey(request.Method))
            {
                return new BadRequestResponse(@$"<h1>{request.Method} is not a supported HttpMethod</h1>");
            }

            if (!this.routes[request.Method].ContainsKey(request.Path))
            {
                return new NotFoundResponse(@$"<h1>{request.Path} does not represent a valid Path</h1>");
            }

            var func = this.routes[request.Method][request.Path];

            return func(request);
        }

        


    }
}
