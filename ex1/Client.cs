using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class Client
    {
        private TcpClient client;
        public Client()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("I'm connected");
        }
        public void SendSomeMessage(string str)
        {
            NetworkStream nwstream = client.GetStream();
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            nwstream.Write(data, 0, data.Length);


        }
    }
}
