using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class SolveGameCommand : ICommand
    {
        private IModel model;
        public SolveGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            MazeSolution solution = model.solveMaze(name, algorithm);
            if(solution == null) { return "maze isn't in solve list"; }
            return solution.ToJSON(name);
        }

        public void setView(IView v)
        {
            throw new NotImplementedException();
        }
    }
}
