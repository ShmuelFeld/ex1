using ex1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    class ClientPool
    {
        /// <summary>
        /// The server
        /// </summary>
        private Server server;
        /// <summary>
        /// The list of clients
        /// </summary>
        private List<ClientDescriptor> listOfClients;
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPool"/> class.
        /// </summary>
        /// <param name="ser">The ser.</param>
        public ClientPool(Server ser)
        {
            this.server = ser;
            this.listOfClients = new List<ClientDescriptor>();
            controller = new Controller();
        }
        /// <summary>
        /// Adds the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="ser">The ser.</param>
        public void addClient(TcpClient client, Server ser)
        {
            ClientDescriptor cli = new ClientDescriptor(client, controller);
            cli.addCommandToClose("generate");
            cli.addCommandToClose("solve");
            cli.addCommandToClose("close");
            this.listOfClients.Add(cli);
          //  cli.addObserver(ser);

        }
        /// <summary>
        /// Closes the communication.
        /// </summary>
        public void closeCommunication()
        {
            foreach (ClientDescriptor item in this.listOfClients)
            {
                item.setClose();
            }
        }
        /// <summary>
        /// Closes the communication.
        /// </summary>
        /// <param name="client">The client.</param>
        public void closeCommunication(TcpClient client)
        {
            foreach (ClientDescriptor item in this.listOfClients)
            {
                if (item.getTcpClient().Equals(client))
                {
                    item.setClose();
                }
            }
        }
    }
}
