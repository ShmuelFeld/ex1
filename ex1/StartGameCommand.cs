using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class StartGameCommand : ICommand
    {
        private IModel model;
        public StartGameCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.startGame(name, rows, cols, client);
            return maze.ToJSON();
        }

        public void setView(IView v)
        {
            throw new NotImplementedException();
        }
    }
}
