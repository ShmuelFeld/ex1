using ex1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server ser = new Server();
            IController controller = new Controller();
            ser.setController(controller);
            IModel model = new Model(controller);
            controller.setModel(model);
            //controller.addCommand("generate", new GenerateMazeCommand(model));
            //controller.addCommand("list", new ListCommand(model));
            //controller.addCommand("solve", new SolveGameCommand(model));
            //controller.addCommand("start", new StartGameCommand(model));
            //controller.addCommand("join", new JoinCommand(model));
            ser.StartToListen();
        }
    }
}
