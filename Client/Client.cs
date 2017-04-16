using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        private TcpClient client;
        private bool endOfCommunication;
        public Client()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("I'm connected");
            this.endOfCommunication = false;
        }
        public void SendSomeMessage(string str)
        {
            Byte[] bytes = new Byte[1024];
            NetworkStream nwstream = client.GetStream();
            while (!this.endOfCommunication)
            {
                string da = Console.ReadLine();
                bytes = System.Text.Encoding.ASCII.GetBytes(da);
                nwstream.Write(bytes, 0, bytes.Length);
                nwstream.Flush();
                bytes.Initialize();
                int i;
                i = nwstream.Read(bytes, 0, bytes.Length);
                da = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine( da);
            }
        }
    }
}
