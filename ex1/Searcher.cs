using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public abstract class Searcher<T> : Isercher<T>
    {
        protected int evaluatedNodes;
        public Searcher()
        {
            evaluatedNodes = 0;
        }
        //ISearcher’s methods:
        public virtual int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        public abstract Solution<T> search(ISearchable<T> isearchable);

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