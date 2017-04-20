
﻿using System;
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
    public class State<T>
    {
        /// <summary>
        /// The state
        /// </summary>
        private T state;
        /// <summary>
        /// The cost
        /// </summary>
        private double cost;
        /// <summary>
        /// The came from
        /// </summary>
        private State<T> cameFrom;
        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public State(T state) 
        {
            this.state = state;
        }
        /// <summary>
        /// Equalses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool Equals(State<T> s) 
        {
            return state.Equals(s.state);
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj != null && state.Equals((obj as State<T>).state);
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.state.ToString().GetHashCode();
        }
        // a property of cost
        /// <summary>
        /// Gets or sets the state of the cost of.
        /// </summary>
        /// <value>
        /// The state of the cost of.
        /// </value>
        public double CostOfState
        {
            get; set;
        }
        // a property of came from
        /// <summary>
        /// Gets or sets the came from.
        /// </summary>
        /// <value>
        /// The came from.
        /// </value>
        public State<T> CameFrom
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public T Instance { get { return state; } set { } }
    }
}