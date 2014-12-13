using System;
using System.Net;
namespace SocketServer
{
    public class ServerUtils
    {
        private string _address;

        public ServerUtils()
        {
            _address = Dns.GetHostName();
        }
        public ServerUtils(string hostName)
        {
            _address = hostName;
        }

        public IPAddress[] GetIP()
        {
            IPAddress[] addressList = null;
            try
            {
                 addressList = Dns.GetHostEntry(_address).AddressList;
            } 
            catch(Exception) 
            {
                // Do nothing
            }
            return addressList;
        }
    }
}
