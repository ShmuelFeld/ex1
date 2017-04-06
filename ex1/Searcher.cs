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

        public Solution<T> backTrace(ISearchable<T> isearchable)
        {
            Solution<T> solution = new Solution<T>();
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
