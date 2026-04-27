using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        string host = "100.91.15.11"; // Tailscale IP of server
        int port = 65432;

        UdpClient client = new UdpClient();

        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(host), port);

        Console.WriteLine("UDP client started.");

        while (true)
        {
            // Send message
            Console.Write("Client: ");
            string message = Console.ReadLine();

            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, serverEndPoint);

            if (message.ToLower() == "exit")
            {
                Console.WriteLine("You ended the chat.");
                break;
            }

            // Receive reply
            IPEndPoint remoteEndPoint = null;
            byte[] response = client.Receive(ref remoteEndPoint);

            string reply = Encoding.UTF8.GetString(response);
            Console.WriteLine("Server: " + reply);

            if (reply.ToLower() == "exit")
            {
                Console.WriteLine("Server ended the chat.");
                break;
            }
        }

        client.Close();
    }
}
