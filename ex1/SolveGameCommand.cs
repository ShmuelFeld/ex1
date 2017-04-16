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
        //TODO
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> solution = model.solveMaze(name, algorithm);
            Solution<Position> mazeSol = solution;
            return mazeSol.ToJSON(name);
        }
    }
}
