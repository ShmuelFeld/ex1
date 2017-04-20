using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ex1;
using Server;
using System.IO;

namespace Client
{
    class Client
    {
        private TcpClient client;
        private bool endOfCommunication;
        IPEndPoint ep;
        public Client()
        {
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("I'm connected");
            this.endOfCommunication = false;
        }

        public void SendSomeMessage(string str)
        {
            NetworkStream stream = client.GetStream();
             StreamReader reader = new StreamReader(stream);
             StreamWriter writer = new StreamWriter(stream);
            {
                while (true)
                {
                    bool isMulti = false;
                    string command = Console.ReadLine();
                    if (!client.Connected)
                    {
                        //IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                        client = new TcpClient();
                        client.Connect(ep);
                        stream = client.GetStream();
                        reader = new StreamReader(stream);
                        writer = new StreamWriter(stream);
                    }
                    if ((command.Contains("start")) || (command.Contains("join")))
                    {
                        isMulti = true;
                    }
                    writer.WriteLine(command);
                    writer.Flush();
                    while (true)
                    {
                        string feedback = reader.ReadLine();
                        if (reader.Peek() == '@')
                        {
                            Console.WriteLine("{0}", feedback);
                            feedback.TrimEnd('\n');
                            break;
                        }
                        Console.WriteLine("{0}", feedback);                        
                    }
                    reader.ReadLine();
                    if (isMulti)
                    {
                        bool close = false;
                        Task sendTask = new Task(() =>
                        {
                            while (!close)
                            {
                                command = Console.ReadLine();
                                if (command.Contains("close")) { close = true; }
                                writer.WriteLine(command);
                                writer.Flush();
                            }
                        });
                        Task listenTask = new Task(() =>
                        {
                            while (!close)
                            {
                                string feedback;
                                while (true)
                                {
                                    feedback = reader.ReadLine();
                                    if (reader.Peek() == '@')
                                    {                                        
                                        Console.WriteLine("{0}", feedback);
                                        feedback.TrimEnd('\n');
                                        break;
                                    }
                                    Console.WriteLine("{0}", feedback);
                                }                                
                                reader.ReadLine();
                            }
                        });
                        sendTask.Start();
                        listenTask.Start();
                        sendTask.Wait();
                        listenTask.Wait();
                    }
                    client.Close();
                    //IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                    //client = new TcpClient();
                    //client.Connect(ep);
                }
            }
        }
    }
}
