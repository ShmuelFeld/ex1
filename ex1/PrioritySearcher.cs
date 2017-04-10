using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace ex1
{
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        protected Priority_Queue.SimplePriorityQueue<State<T>> open;
        public PrioritySearcher()
        {
            open = new Priority_Queue.SimplePriorityQueue<State<T>>();
        }
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return open.Dequeue();
        }
        // a property of openList
        public int OpenListSize
        { // it is a read-only property :)
            get { return open.Count; }
        }
        //ISearcher’s methods:
        //public virtual int getNumberOfNodesEvaluated()
        //{
        //    return evaluatedNodes;
        //}
        //public abstract Solution<T> search(ISearchable<T> isearchable);
        public void addToOpenList(State<T> s)
        {
            open.Enqueue(s, (float)s.CostOfState);
        }

    }
}