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
        public HttpServer(string ipAddress, int port)
        {
            var tempIp = IPAddress.Parse(ipAddress);
            serverListener = new TcpListener(tempIp, port);
            Console.WriteLine($"Server started on port {port}, waiting for client connection...");
            serverListener.Start();
        }

        public HttpServer()
            :this(ipAddress, defaultPort)
        {
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

                var request = await GetRequest(networkStream);
                Console.WriteLine();
                Console.WriteLine("RECEIVED REQUEST:");
                Console.WriteLine(request);


                Console.WriteLine();
                Console.WriteLine("SENDING RESPONSE:");
                var content = @"<head></head>
<body>
<h1>Hello From My Server!</h1>
</body>";
                var response = SetResponse(content);
                Console.WriteLine(Encoding.UTF8.GetString(response));

                await networkStream.WriteAsync(response);

                connection.Close();
            }
        }

        public async Task<string> GetRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];
            var sb1 = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                sb1.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }

            return sb1.ToString();
        }

        public byte[] SetResponse(string content)
        {
            var contentLength = Encoding.UTF8.GetByteCount(content);
            var sb2 = new StringBuilder();
            sb2.AppendLine("HTTP/1.1 200 OK");
            sb2.AppendLine("Server: SoftUni Web Server");
            sb2.AppendLine($"Date: {DateTime.UtcNow.ToString("r")}");
            sb2.AppendLine($"Content-Length: {contentLength}");
            sb2.AppendLine("Content-Type: text/html; charset=UTF-8");
            sb2.AppendLine();
            sb2.AppendLine(content);

            var response = Encoding.UTF8.GetBytes(sb2.ToString());
            return response;
        }
    }
}
