using ex1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server: IObserver
    {
        private IController controller;
        private TcpListener listener;
        private ClientPool clientPool;
        private bool endOfCommunication;
        public Server()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            listener = new TcpListener(ep);
            this.clientPool = new ClientPool(this);
            this.endOfCommunication = false;
            listener.Start();
        }

        public void setController(IController cntrl)
        {
            controller = cntrl;
        }
        public void newMessageArrived(string command, IObservable observable)
        {
            ClientDescriptor cd = observable as ClientDescriptor;
            TcpClient tcp = cd.getTcpClient();
            string result = controller.ExecuteCommand(command, tcp);
            Console.WriteLine(result);
            cd.sendToClient(result);
        }
        public void StartToListen()
        {
            Task listen = new Task(() =>
            {
                TcpClient cli = listener.AcceptTcpClient();
                while (cli != null) {
                    if (cli != null)
                    {
                        Console.WriteLine("Waiting for client connections...");
                        this.clientPool.addClient(cli, this);
                        cli = null;
                    }
            }
                //this.clientPool.closeCommunication();
            });
            listen.Start();
            while(true){ }
        }
    }
}
