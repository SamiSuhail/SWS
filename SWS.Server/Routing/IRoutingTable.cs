using SWS.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server.Routing
{
    interface IRoutingTable
    {
        IRoutingTable Map(string url, HttpResponse response);
        IRoutingTable Map(string url, HttpMethod method, HttpResponse response);
        IRoutingTable MapGet(string url, HttpResponse response);
    }
}
