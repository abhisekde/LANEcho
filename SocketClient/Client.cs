using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    public class Client
    {
        private TcpClient _tcpClient;
        private IPAddress _IP;
        private IPEndPoint _ServerEndpoint;
        private int _port = 5050;

        // Create the client
        public Client()
        {
            _tcpClient = new TcpClient();
            _IP = IPAddress.Loopback;
            _ServerEndpoint = new IPEndPoint(_IP, _port);
        }

        // Create the client
        public Client(string IP, int port)
        {
            if (!IPAddress.TryParse(IP, out _IP)) 
                _IP = IPAddress.Loopback;
            _port = port;
            _tcpClient = new TcpClient();
            _ServerEndpoint = new IPEndPoint(_IP, _port);
        }

        // Start client
        public void Start()
        {
            try
            {
                _tcpClient.Connect(_ServerEndpoint);
                DataTransaction(_tcpClient);
                _tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private void DataTransaction(TcpClient tcpClient)
        {
            bool flag = true;
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = new byte[4096];
            byte[] response = new byte[4096];
            int responseLength;
            NetworkStream clientStream = tcpClient.GetStream();
            while (flag)
            {
                try
                {
                    Console.Write(">> ");
                    var text = Console.ReadLine();
                    buffer = encoder.GetBytes(text);
                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();

                    responseLength = clientStream.Read(response, 0, response.Length);
                    Console.Write("<< ");
                    encoder.GetString(response, 0, responseLength);
                    Console.WriteLine(text);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}
