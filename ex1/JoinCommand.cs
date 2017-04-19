using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class JoinCommand : ICommand
    {
        private IModel model;
        public JoinCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            Maze joinedMaze = model.join(name, client);
            if (joinedMaze == null)
            {
                return "maze isn't in join list";
            }
            return joinedMaze.ToJSON();
        }

        public void setView(IView v)
        {
            throw new NotImplementedException();
        }
    }
}
