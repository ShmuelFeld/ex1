using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public interface ISearchable<T>
    {
        State<T> getInitialState();
        State<T> getGoalState();
        Dictionary<State<T>, double> getAllPossibleStates(State<T> s);
    }
}
