using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SWS.Server;

namespace SWS
{
    public class StartUp
    {
        static async Task Main()
        {
            var server = new HttpServer();

            await server.Start();
        }
    }
}
