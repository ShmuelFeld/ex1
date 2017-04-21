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
    /// <summary>
    /// 
    /// </summary>
    class Server
    {
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;
        /// <summary>
        /// The listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The client pool
        /// </summary>
        private ClientPool clientPool;
        /// <summary>
        /// The end of communication
        /// </summary>
        private bool endOfCommunication;
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            listener = new TcpListener(ep);
            this.clientPool = new ClientPool(this);
            this.endOfCommunication = false;
            listener.Start();
        }

        /// <summary>
        /// Sets the controller.
        /// </summary>
        /// <param name="cntrl">The CNTRL.</param>
        public void SetController(IController cntrl)
        {
            controller = cntrl;
        }
        /// <summary>
        /// Starts to listen.
        /// </summary>
        public void StartToListen()
        {
            Task listen = new Task(() =>
            {
                while (true) { 
                TcpClient cli = listener.AcceptTcpClient();
                    if (cli != null)
                    {
                        this.clientPool.AddClient(cli, this);
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
