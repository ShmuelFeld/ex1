using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ex1.ICommand" />
    class CloseCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// The view
        /// </summary>
        private IView view;
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand (IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            TcpClient otherClient = model.close(client);
            view.sendToOtherClient("close your server", otherClient);
            return "close";
        }

        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="v">The v.</param>
        public void setView(IView v) { view = v; }
    }
}
