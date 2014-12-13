using System;
using System.Net;
using SocketServer;
namespace SocketBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress []addressList;
            ServerUtils utils = new ServerUtils();
            addressList = utils.GetIP();
            foreach (IPAddress addr in addressList)
            {
                Console.WriteLine(addr);
            }
            Console.ReadKey();
        }
    }
}
