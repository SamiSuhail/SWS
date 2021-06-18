using Common;
using SWS.Server.Http;
using SWS.Server.Routing;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SWS.Server
{
    public class HttpServer
    {
        private const string ipAddress = "127.0.0.1";
        private const int defaultPort = 9090;
        private readonly TcpListener serverListener;
        private readonly IRoutingTable routingTable = new RoutingTable();
        public HttpServer(string ipAddress, int port)
        {
            Guard.AgainstNull(ipAddress, "Server IP Address");
            Guard.AgainstNull(port, "Server Port");

            var tempIp = IPAddress.Parse(ipAddress);
            serverListener = new TcpListener(tempIp, port);
            Console.WriteLine($"Server started on port {port}, waiting for client connection...");
            serverListener.Start();
        }

        public HttpServer()
            : this(ipAddress, defaultPort)
        {
        }

        public HttpServer(Action<IRoutingTable> routingAction)
            : this(ipAddress, defaultPort)
        {
            routingAction(this.routingTable);
        }

        public async Task Start()
        {
            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();
                Console.WriteLine();
                Console.WriteLine("NEW CONNECTION STARTED");
                Console.WriteLine();
                var networkStream = connection.GetStream();

                var request = await ReadRequst(networkStream);
                Console.WriteLine();
                Console.WriteLine("RECEIVED REQUEST:");
                Console.WriteLine(request.ToString());


                Console.WriteLine();
                Console.WriteLine("SENDING RESPONSE:");
                var response = routingTable.MatchRequest(request);
                Console.WriteLine(response.ToString());
                await WriteResponse(networkStream, response);

                connection.Close();
            }
        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(responseBytes);
        }

        public async Task<HttpRequest> ReadRequst(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];
            var sb1 = new StringBuilder();


            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                sb1.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            } while (networkStream.DataAvailable);

                return HttpRequest.Parse(sb1.ToString());
        }

    }
}
