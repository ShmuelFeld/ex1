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
    public interface Isercher<T>
    {
        // the search method
        /// <summary>
        /// Searches the specified isearchable.
        /// </summary>
        /// <param name="isearchable">The isearchable.</param>
        /// <returns></returns>
        Solution<T> search(ISearchable<T> isearchable);/// <summary>
        // get how many nodes were evaluated by the algorithm
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        int getNumberOfNodesEvaluated();
    }
}