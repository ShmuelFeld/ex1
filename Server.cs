using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class Server
    {
        private TcpListener listener;
        public Server() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            listener = new TcpListener(ep);
            listener.Start();
        }
        public void StartToListen() {
            Console.WriteLine("Waiting for client connections...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");
            NetworkStream stream = client.GetStream();
            Console.WriteLine(stream);
            
        }
            public TcpListener Litener { get => listener; set => listener = value; }
        public TcpListener Litener1 { get => listener; set => listener = value; }
    }
}
