using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ex1.ICommand" />
    public class SolveGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveGameCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            MazeSolution solution = model.SolveMaze(name, algorithm);
            if(solution == null) { return "maze isn't in solve list"; }
            return solution.ToJSON(name);
        }

        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetView(IView v)
        {
            throw new NotImplementedException();
        }
    }
}
