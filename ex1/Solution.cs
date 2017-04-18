using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class Solution<T>
    {
        protected Stack<State<T>> backTrace;
        public Solution()
        {
            backTrace = new Stack<State<T>>();
        }
        // a property of backTrace
        // public Stack<State<T>> BackTrace { get; }
        public void add(State<T> s)
        {
            backTrace.Push(s);
        }
        public int numOfStates()
        {
            return backTrace.Count();
        }
        //public string ToString() { return null; }
        //public string ToJSON(string name)
        //{
        //    JObject solveObj = new JObject();
        //    solveObj["Name"] = name;
        //    solveObj["solution"] = ToString();
        //    solveObj["NodesEvaluated"] = numOfStates();
        //    return solveObj.ToString();
        //}
        public Stack<State<T>> getBackTrace() { return backTrace; }
    }
}