using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// News the message arrived.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="observable">The observable.</param>
        void NewMessageArrived(string command, IObservable observable);
    }
}
