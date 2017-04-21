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
    /// <seealso cref="ex1.Isercher{T}" />
    public abstract class Searcher<T> : Isercher<T>
    {
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        protected int evaluatedNodes;
        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }
        //ISearcher’s methods:
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public virtual int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /// <summary>
        /// Searches the specified isearchable.
        /// </summary>
        /// <param name="isearchable">The isearchable.</param>
        /// <returns></returns>
        public abstract Solution<T> search(ISearchable<T> isearchable);

        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="init">The initialize.</param>
        /// <param name="goal">The goal.</param>
        /// <returns></returns>
        public Solution<T> backTrace(State<T> init, State<T> goal) 
        {
            Solution<T> solution = new Solution<T>();
            while (!goal.Equals(init))
            {
                solution.add(goal);
                goal = goal.CameFrom;
            }
            //add the initial state
            solution.add(goal);
            return solution;
        }

    }
}