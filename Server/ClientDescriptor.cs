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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ex1.IView" />
    class ClientDescriptor : IView
    {
        /// <summary>
        /// The TCP
        /// </summary>
        private TcpClient tcp;
        /// <summary>
        /// The task
        /// </summary>
        private Task task;
        /// <summary>
        /// The end of communication
        /// </summary>
        private bool endOfCommunication;
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;
        /// <summary>
        /// The commands to close
        /// </summary>
        private List<string> commandsToClose;
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDescriptor"/> class.
        /// </summary>
        /// <param name="tc">The tc.</param>
        /// <param name="cntrlr">The CNTRLR.</param>
        public ClientDescriptor(TcpClient tc, IController cntrlr)
        {
            this.tcp = tc;
            controller = cntrlr;
            controller.SetView(this);
            this.endOfCommunication = false;
            commandsToClose = new List<string>();
            StartListening();
        }
        /// <summary>
        /// Adds the command to close.
        /// </summary>
        /// <param name="command">The command.</param>
        public void AddCommandToClose(string command) { commandsToClose.Add(command); }
        /// <summary>
        /// Starts the listening.
        /// </summary>
        public void StartListening()
        {
            this.task = new Task(() =>{
                using (NetworkStream stream = tcp.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (!endOfCommunication) 
                    {
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
                }
            });
            task.Start();
        }
        /// <summary>
        /// Closes the client.
        /// </summary>
        public void CloseClient()
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
        /// <summary>
        /// Sends to other client.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="otherClient">The other client.</param>
        public void SendToOtherClient(string data, TcpClient otherClient)
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
        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <returns></returns>
        public Task Task
        {
            get
            {
                return this.task;
            }
        }

        /// <summary>
        /// Gets the TCP client.
        /// </summary>
        /// <returns></returns>
        public TcpClient TcpClient
        {
            get
            {
                return this.tcp;
            }
        }

        /// <summary>
        /// Sets the close.
        /// </summary>
        public void SetClose()
        {
            this.endOfCommunication = true;
        }

    }
}
