using ex1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientDescriptor: IView
    {
        private TcpClient tcp;
        private Task task;
        private bool endOfCommunication;
        private IController controller;
        private List<string> commandsToClose;
        //private List<IObserver> observers;
        public ClientDescriptor(TcpClient tc, IController cntrlr)
        {
            this.tcp = tc;
            controller = cntrlr;
            controller.setView(this);
            this.endOfCommunication = false;
            commandsToClose = new List<string>();
            //this.observers = new List<IObserver>();
            startListening();
        }
        public void addCommandToClose(string command) { commandsToClose.Add(command); }
        public void startListening()
        {
            this.task = new Task(() =>{
                using (NetworkStream stream = tcp.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (!endOfCommunication) 
                    {
                        //bool isToClose = false;
                        string commandLine = reader.ReadLine();
                        if(commandLine == "close your server")
                        {
                            endOfCommunication = true;
                            break;
                        }
                        foreach (string command in commandsToClose)
                        {
                            if (commandLine.Contains(command))
                            {
                                endOfCommunication = true;
                            }
                        }
                        string result = controller.ExecuteCommand(commandLine, tcp);
                        Console.WriteLine(result);
                        result += '\n';
                        result += '@';
                        writer.WriteLine(result);
                        writer.Flush();
                        if (result.Contains("close")) { endOfCommunication = true; }
                    }
                    Console.WriteLine("client handler done");
                }
            });
            task.Start();
        }
        public void closeClient()
        {
            NetworkStream stream = tcp.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            {
                string data = "";
                Console.WriteLine("closing client");
                data += '\n';
                data += '@';
                writer.WriteLine(data);
                writer.Flush();
            }
        }
        public void sendToOtherClient(string data, TcpClient otherClient)
        {
            NetworkStream stream = otherClient.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            {
                    Console.WriteLine(data);
                    data += '\n';
                    data += '@';
                    writer.WriteLine(data);
                    writer.Flush();
            }
        }
        public Task getTask()
        {
            return this.task;
        }
        public TcpClient getTcpClient()
        {
            return this.tcp;
        }
        public void setClose()
        {
            this.endOfCommunication = true;
        }

        //public void addObserver(IObserver observer)
        //{
        //    this.observers.Add(observer);
        //}
         
        //public void notifyObservers(string str)
        //{
        //    foreach (IObserver item in this.observers)
        //    {
        //        item.newMessageArrived(str, this);
        //    }
        //}

        //public void sendToClient(string data)
        //{
        //    using (NetworkStream stream = tcp.GetStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    using (StreamWriter writer = new StreamWriter(stream))
        //    {
        //            data += '\n';
        //            data += '@';
        //            writer.WriteLine(data);
        //            writer.Flush();
        //    }
        //}
    }
}
