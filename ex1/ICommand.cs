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
    public interface ICommand
    {
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        string Execute(string[] args, TcpClient client);
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="v">The v.</param>
        void SetView(IView v);
    }
}
