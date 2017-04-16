using System;
using System.Collections.Generic;
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
                Byte[] bytes = new Byte[256];
                NetworkStream stream = this.tcp.GetStream();
                int i;
                while (!this.endOfCommunication)
                {
                    if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);//server prints to screen
                        stream.Flush();
                        //bytes = System.Text.Encoding.ASCII.GetBytes(data);
                        //stream.Write(bytes, 0, bytes.Length);
                        //stream.Flush();
                        notifyObservers(data);
                    }
                }
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
    }
}
