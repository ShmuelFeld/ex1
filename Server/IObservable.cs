using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IObservable
    {
        void addObserver(IObserver observer);
        void notifyObservers(string str);
    }
}
