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
            var server = new HttpServer(routes => routes.MapGet("/", new HtmlResponse("<h1>Welcome to the home page</h1>"))
                                                    .MapGet("/Cats", request => {
                                                        var catName = "all of our cats";


                                                        if (request.Query != null && request.Query.ContainsKey("Name"))
                                                        {
                                                            catName = request.Query["Name"];
                                                        }

                                                        return new HtmlResponse($"<h1>Welcome to the Cats page, you are currently looking at {catName}!</h1>");
                                                    }));
            

            await server.Start();
        }
    }
}
