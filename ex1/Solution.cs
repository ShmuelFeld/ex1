using Newtonsoft.Json.Linq;
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
    public class Solution<T>
    {
        /// <summary>
        /// The back trace
        /// </summary>
        protected Stack<State<T>> backTrace;
        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        public Solution()
        {
            backTrace = new Stack<State<T>>();
        }
        // a property of backTrace
        // public Stack<State<T>> BackTrace { get; }
        /// <summary>
        /// Adds the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        public void add(State<T> s)
        {
            backTrace.Push(s);
        }
        /// <summary>
        /// Numbers the of states.
        /// </summary>
        /// <returns></returns>
        public int numOfStates()
        {
            return backTrace.Count();
        }
        /// <summary>
        /// Gets the back trace.
        /// </summary>
        /// <returns></returns>
        public Stack<State<T>> getBackTrace() { return backTrace; }
    }
}