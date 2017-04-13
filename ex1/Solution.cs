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
        private Stack<State<T>> backTrace;
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
        public string ToString()
        {
            string solution;
            Stack<State<T>> temp = backTrace;
            State<T> prev = temp.First();
            //while (!temp.Any())
            //{
            //    State<T> cur = temp.First();

            //}
            return "solution in progress......";
        }
        public string ToJSON(string name)
        {
            JObject solveObj = new JObject();
            solveObj["Name"] = name;
            solveObj["solution"] = ToString();
            solveObj["NodesEvaluated"] = numOfStates();
            return solveObj.ToString();
        }
    }
}