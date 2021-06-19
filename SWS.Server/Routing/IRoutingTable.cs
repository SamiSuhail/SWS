using SWS.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);
        IRoutingTable MapGet(string path, HttpResponse response);
        IRoutingTable MapPost(string path, HttpResponse response);
        IRoutingTable MapPut(string path, HttpResponse response);
        IRoutingTable MapDelete(string path, HttpResponse response);

        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunc);
        IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunc);
        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunc);
        IRoutingTable MapPut(string path, Func<HttpRequest, HttpResponse> responseFunc);
        IRoutingTable MapDelete(string path, Func<HttpRequest, HttpResponse> responseFunc);
        public HttpResponse ExecuteRequest(HttpRequest request);
    }
}
