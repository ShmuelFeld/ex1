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
    public interface IView
    {
        /// <summary>
        /// Sends to other client.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="otherClient">The other client.</param>
        void sendToOtherClient(string data, TcpClient otherClient);
    }
}
