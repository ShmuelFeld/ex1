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
    /// <seealso cref="ex1.Searcher{T}" />
    class DFS<T> : Searcher<T>
    {
        /// <summary>
        /// The stack
        /// </summary>
        HashSet<State<T>> stack;
        /// <summary>
        /// Initializes a new instance of the <see cref="DFS{T}"/> class.
        /// </summary>
        public DFS()
        {
            stack = new HashSet<State<T>>();
        }
        /// <summary>
        /// Searches the specified isearchable.
        /// </summary>
        /// <param name="isearchable">The isearchable.</param>
        /// <returns></returns>
        public override Solution<T> search(ISearchable<T> isearchable)
        {
            HashSet<State<T>> visited = new HashSet<State<T>>();
            stack.Add(isearchable.getInitialState());
           // visited.Add(isearchable.getInitialState());
            while (stack.Count() != 0)
            {
                State<T> state = stack.Last();
                stack.Remove(state);
                if (!visited.Contains(state))
                {
                    visited.Add(state);
                    Dictionary<State<T>, double> succerssors = isearchable.getAllPossibleStates(state);
                    foreach (KeyValuePair<State<T>, double> s in succerssors)
                    {
                        stack.Add(s.Key);
                        s.Key.CameFrom = state;
                        if(s.Key.Equals(isearchable.getGoalState()))
                        {
                            return backTrace(isearchable.getInitialState(), s.Key);
                        }
                    }
                }
            }
            return null;
        }
    }
}