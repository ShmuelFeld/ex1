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
    /// <seealso cref="ex1.PrioritySearcher{T}" />
    public class BFS<T> : PrioritySearcher<T>
    {
        /// <summary>
        /// The closed
        /// </summary>
        private HashSet<State<T>> closed = new HashSet<State<T>>();
        /// <summary>
        /// Searches the specified isearchable.
        /// </summary>
        /// <param name="isearchable">The isearchable.</param>
        /// <returns></returns>
        public override Solution<T> search(ISearchable<T> isearchable)
        {
            addToOpenList(isearchable.getInitialState()); // inherited from Searcher

            while (OpenListSize > 0)
            {
                State<T> n = popOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(isearchable.getGoalState()))
                    return backTrace(isearchable.getInitialState(), n, isearchable.getEvauatedNodes()); // private method, back traces through the parents
                                                   // calling the delegated method, returns a list of states with n as a parent
                Dictionary<State<T>, double> succerssors = isearchable.getAllPossibleStates(n);
                foreach (KeyValuePair<State<T>, double> s in succerssors)
                {
                    if (!(closed.Contains(s.Key)) && !(open.Contains(s.Key)))
                    {
                        s.Key.CameFrom = n;
                        //s.CameFrom = n; // already done by getSuccessors
                        s.Key.CostOfState = s.Value;
                        addToOpenList(s.Key);
                    }
                    else if (open.Contains(s.Key))
                    {
                        //relief
                        if ((s.Value + n.CostOfState) < s.Key.CostOfState)
                        {
                            s.Key.CameFrom = n;
                            s.Key.CostOfState = s.Value + n.CostOfState;
                            open.UpdatePriority(s.Key, (float)(s.Key.CostOfState));
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="isearchable">The isearchable.</param>
        /// <returns></returns>
        private Solution<T> backTrace(ISearchable<T> isearchable)
        {
            Solution<T> solution = new Solution<T>(isearchable.getEvauatedNodes());
            State<T> state = isearchable.getGoalState();
            while (!state.Equals(isearchable.getInitialState()))
            {
                solution.add(state);
                state = state.CameFrom;
            }
            //add the initial state
            solution.add(state);
            return solution;
        }
  }
        
}
