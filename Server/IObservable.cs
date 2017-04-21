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
    public interface IObservable
    {
        /// <summary>
        /// Adds the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void AddObserver(IObserver observer);
        /// <summary>
        /// Notifies the observers.
        /// </summary>
        /// <param name="str">The string.</param>
        void NotifyObservers(string str);
    }
}
