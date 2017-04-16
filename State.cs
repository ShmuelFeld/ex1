
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class State<T>
    {
        private T state; // the state represented by a string
        private double cost; // cost to reach this state (set by a setter)
        private State<T> cameFrom; // the state we came from to this state (setter)
        public State(T state) // CTOR
        {
            this.state = state;
        }
        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }
        public override bool Equals(object obj)
        {
            return obj != null && state.Equals((obj as State<T>).state);
        }
        public override int GetHashCode()
        {
            return this.state.ToString().GetHashCode();
        }
        // a property of cost
        public double CostOfState
        {
            get; set;
            //get { return cost; }
            //set { }
        }
        // a property of came from
        public State<T> CameFrom
        {
            get; set;
            //get { return cameFrom; }
            //set { }
        }
        public T Instance { get { return state; } set { } }
    }
}