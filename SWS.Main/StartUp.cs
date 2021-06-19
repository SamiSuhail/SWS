using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SWS.Controllers;
using SWS.Server;
using SWS.Server.Http;
using SWS.Server.Responses;

namespace SWS
{
    public class StartUp
    {
        static async Task Main()
        {
            var server = new HttpServer(routes => routes.MapGet("/", request => new HomeController(request).Index())
                                                    .MapGet("/Cats", request => new AnimalsController(request).Cats())
                                                    .MapGet("/Dogs", request => new AnimalsController(request).Dogs()));
            

            await server.Start();
        }
    }
}
