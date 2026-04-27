using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        string host = "100.91.15.11";
        int port = 65432;

        try
        {
            using (TcpClient client = new TcpClient(host, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Connected to server.");

                while (true)
                {
                    // Send message first
                    Console.Write("Client: ");
                    string message = Console.ReadLine();

                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    if (message.ToLower() == "exit")
                    {
                        Console.WriteLine("You ended the chat.");
                        break;
                    }

                    // Receive response
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    string reply = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Server: " + reply);

                    if (reply.ToLower() == "exit")
                    {
                        Console.WriteLine("Server ended the chat.");
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("Connection closed.");
    }
}
