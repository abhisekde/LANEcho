using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;

namespace SocketServer
{
    // This is an echo server
    public class Server
    {
        private TcpListener _tcpListener;
        private IPAddress _IP;
        private int _port = 5050;
        private int _threadCount; // no. of clients

        // Create the server
        public Server()
        {
            _tcpListener = new TcpListener(IPAddress.Loopback, _port);
            _threadCount = 0;
        }

        // Create the server
        public Server(string IP, int port)
        {
            if (!IPAddress.TryParse(IP, out _IP)) 
                _IP = IPAddress.Loopback;
            _port = port;
            _tcpListener = new TcpListener(_IP, _port);
            _threadCount = 0;
        }

        // The task thread
        private void Service()
        {
            _tcpListener.Start();
            while (true)
            {
                // Wait for a client
                TcpClient client = _tcpListener.AcceptTcpClient();
                ++_threadCount;

                // Client thread
                Thread clientThread = new Thread(new ParameterizedThreadStart(ClientHandler));
                clientThread.Start(client);
            }
        }

        // Client handler
        private void ClientHandler(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            NetworkStream clientStream = tcpClient.GetStream();
            int SIZE = 4096;
            byte[] msg = new byte[SIZE];
            int byteCount;
            while (true)
            {
                byteCount = 0;
                try
                {
                    byteCount = clientStream.Read(msg, 0, SIZE);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    break;
                }
                if (byteCount == 0)
                {
                    --_threadCount;
                    break;
                }

                ASCIIEncoding encoder = new ASCIIEncoding();
                string text = encoder.GetString(msg, 0, byteCount);
                WriteMessage(text);
                
                Echo(msg, text, byteCount, clientStream);
            }
            tcpClient.Close();
        }

        // Echo back the text
        private void Echo(byte[] msg, string text, int byteCount, NetworkStream clientStream)
        {
            Console.WriteLine(text);
            clientStream.Write(msg, 0, byteCount);
            clientStream.Flush();
        }

        private void WriteMessage(string text)
        {
            Console.Write("<< ");
            Console.WriteLine(text);
            Console.Write(">> ");
        }

        // Start the server
        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(Service));
            thread.Start();
        }
    }
}
