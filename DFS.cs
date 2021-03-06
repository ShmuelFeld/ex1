﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class DFS<T> : Searcher<T>
    {
        HashSet<State<T>> stack;
        public DFS()
        {
            stack = new HashSet<State<T>>();
        }
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