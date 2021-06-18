using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SWS.Server;
using SWS.Server.Http;
using SWS.Server.Responses;

namespace SWS
{
    public class StartUp
    {
        static async Task Main()
        {
            var server = new HttpServer();
                //routes => routes.MapGet("/", new TextResponse("Welcome to the home page"))
                //                                        .MapGet("/Cats", new TextResponse("Welcome to the cats page")));

            await server.Start();
        }
    }
}
