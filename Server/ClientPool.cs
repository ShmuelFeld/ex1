﻿using ex1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientPool
    {
        private Server server;
        private List<ClientDescriptor> listOfClients;
        private IController controller;

        public ClientPool(Server ser)
        {
            this.server = ser;
            this.listOfClients = new List<ClientDescriptor>();
            controller = new Controller();
        }
        public void addClient(TcpClient client, Server ser)
        {
            ClientDescriptor cli = new ClientDescriptor(client, controller);
            cli.addCommandToClose("generate");
            cli.addCommandToClose("solve");
            cli.addCommandToClose("close");
            this.listOfClients.Add(cli);
          //  cli.addObserver(ser);

        }
        public void closeCommunication()
        {
            foreach (ClientDescriptor item in this.listOfClients)
            {
                item.setClose();
            }
        }
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
