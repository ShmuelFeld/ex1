using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;

namespace ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            //compareSolvers();
            part2();
        }
        static void part2()
        {
            IController controller = new Controller();
            IModel model = new Model(controller);
            controller.setModel(model);
            controller.addCommand("generate", new GenerateMazeCommand(model));
            controller.ExecuteCommand("generate maze1 4 4");
            controller.ExecuteCommand("generate maze2 4 4");
            controller.addCommand("list", new ListCommand(model));
            Console.WriteLine(controller.ExecuteCommand("list"));
            //Console.WriteLine(controller.ExecuteCommand("generate maze1 4 4"));
        }
        static void compareSolvers()
        {
            DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
            Maze maze = dfsMaze.Generate(500,500);
            Console.WriteLine(maze.ToString());
            IsearchableMaze ism = new IsearchableMaze(maze);
            BFS<Position> bfs = new BFS<Position>();
            Console.WriteLine(bfs.search(ism).numOfStates());
            DFS<Position> dfs = new DFS<Position>();
            Console.WriteLine(dfs.search(ism).numOfStates());
        }
    }
}
