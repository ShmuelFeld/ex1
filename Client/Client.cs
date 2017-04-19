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
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                while (true)
                {
                    bool isMulti = false;
                    string command = Console.ReadLine();
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
                    //if ((command.Contains("start")) || (command.Contains("join")))
                    if (isMulti)
                    {
                        Task listenTask = new Task(() =>
                        {
                            while (true)
                            {
                                command = Console.ReadLine();
                                writer.WriteLine(command);
                                writer.Flush();
                            }
                        });
                        Task sendTask = new Task(() =>
                        {
                            while (true)
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
                }
            }
        }
    }
}
