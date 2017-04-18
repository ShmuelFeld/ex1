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
    class ClientDescriptor: IObservable
    {
        private TcpClient tcp;
        private Task task;
        private bool endOfCommunication;
        private List<IObserver> observers;
        public ClientDescriptor(TcpClient tc)
        {
            this.tcp = tc;
            this.endOfCommunication = false;
            this.observers = new List<IObserver>();
            startListening();
        }
        public void startListening()
        {
            this.task = new Task(() =>{
                using (NetworkStream stream = tcp.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (true)
                    {
                        string commandLine = reader.ReadLine();
                        //notifyObservers(commandLine);
                        IController c = new Controller();
                        string result = c.ExecuteCommand(commandLine, tcp);
                        Console.WriteLine(result);
                        result += '\n';
                        result += '@';
                        writer.WriteLine(result);
                       // writer.Flush();
                        writer.Flush();
                        //Console.WriteLine(commandLine);
                        //string result = controller.executeCommand(commandLine, client);
                        //Console.WriteLine("the result we wanna send: {0}", result);
                        //result += '\n';
                        //result += '@';
                        //writer.WriteLine(result);
                        //writer.Flush();
                    }
                }
                //Byte[] bytes = new Byte[1024];
                //NetworkStream stream = this.tcp.GetStream();
                //int i;
                //while (!this.endOfCommunication)
                //{
                //    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                //    {
                //        string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                //        Console.WriteLine("Received: {0}", data);
                //        stream.Flush();

                //        //bytes = System.Text.Encoding.ASCII.GetBytes(data);
                //        //stream.Write(bytes, 0, bytes.Length);
                //        //stream.Flush();
                //        notifyObservers(data);

                //    }
            });
            task.Start();
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

        public void addObserver(IObserver observer)
        {
            this.observers.Add(observer);
        }
         
        public void notifyObservers(string str)
        {
            foreach (IObserver item in this.observers)
            {
                item.newMessageArrived(str, this);
            }
        }

        public void sendToClient(string data)
        {
            using (NetworkStream stream = tcp.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
               // while (!this.endOfCommunication)
               // {
                    data += '\n';
                    data += '@';
                    writer.WriteLine(data);
                    writer.Flush();
               // }
            }
            //Byte[] bytes = new Byte[1024];
            //using (NetworkStream stream = tcp.GetStream())
            //using (BinaryReader reader = new BinaryReader(stream))
            //using (BinaryWriter writer = new BinaryWriter(stream))            //{
            //    bytes = System.Text.Encoding.ASCII.GetBytes(data);
            //    writer.Write(bytes, 0, bytes.Length);
            //    stream.Flush() ;
            //    writer.Flush();
            //}
        }
    }
}
