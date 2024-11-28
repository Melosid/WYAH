using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace WYAH
{
    public class Client
    {
        public async Task<Response> GetIt()
        {
            string url = "/comments";
            string host = "jsonplaceholder.typicode.com";

            IPHostEntry iPHostInfo = await Dns.GetHostEntryAsync(host);
            IPAddress ipAddress = iPHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new(ipAddress, 80);

            using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(ipEndPoint);
            var request = new Request(url, host);
            request.addHeader("host", host);
            request.addQueryParam("postId", "1");

            var reqMessage = request.serialize();
            var messageBytes = Encoding.UTF8.GetBytes(reqMessage);
            _ = await client.SendAsync(messageBytes);
            Console.WriteLine($"Socket client sent message: \"{reqMessage}\"");
            var resMessage = "";
            while (true)
            {
                var buffer = new byte[1024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var res = Encoding.UTF8.GetString(buffer, 0, received);
                resMessage += res;
                if (res == "") break;
            }
            var response = new Response(resMessage);
            client.Shutdown(SocketShutdown.Both);

            return response;
        }
    }
}