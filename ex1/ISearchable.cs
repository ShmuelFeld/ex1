using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<T> getInitialState();
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        State<T> getGoalState();
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        Dictionary<State<T>, double> getAllPossibleStates(State<T> s);
    }
}
