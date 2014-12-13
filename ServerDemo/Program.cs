using System;
using SocketServer;
namespace ServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chat Server");
            Server server = new Server();
            server.Start();
        }
    }
}
