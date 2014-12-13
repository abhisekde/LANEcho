using SocketClient;
using System;

namespace ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chat Client");
            Client client = new Client();
            client.Start();
        }
    }
}
