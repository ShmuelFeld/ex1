using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public interface Isercher<T>
    {
        // the search method
        Solution<T> search(ISearchable<T> isearchable);
        // get how many nodes were evaluated by the algorithm
        int getNumberOfNodesEvaluated();
    }
}
