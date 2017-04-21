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
    public class JoinCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinCommand(IModel model)
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
            Maze joinedMaze = model.Join(name, client);
            if (joinedMaze == null)
            {
                return "maze isn't in join list";
            }
            return joinedMaze.ToJSON();
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
