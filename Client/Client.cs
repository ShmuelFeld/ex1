﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ex1;
using Server;

namespace Client
{
    class Client : IObserver 
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

        public void newMessageArrived(string command, IObservable observable)
        {
            throw new NotImplementedException();
        }

        public void SendSomeMessage(string str)
        {
            Byte[] bytes = new Byte[1024];
            NetworkStream nwstream = client.GetStream();
            while (!this.endOfCommunication)
            {
                string da = Console.ReadLine();
                bytes = System.Text.Encoding.ASCII.GetBytes(da);
                string[] arr = da.Split(' ');
                string commandKey = arr[0];
                nwstream.Write(bytes, 0, bytes.Length);
                nwstream.Flush();
                bytes.Initialize();
                int i;
                i = nwstream.Read(bytes, 0, bytes.Length);
                da = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine(da);
                if ((da == "generate") || (da == "solve"))
                {
                    client = new TcpClient();
                    nwstream = client.GetStream();
                }
            }
        }
    }
}
