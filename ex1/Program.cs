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
            Console.WriteLine("Hello world");
            compareSolvers();
        }
        static void compareSolvers()
        {
            //bool i = true;
            //Task t = new Task(()=>{
            //    while (i)
            //    {
            //        Console.WriteLine("in while");
            //    }
            //});
            //t.Start();
            //Console.WriteLine("thread start");

            //int l;
            //for (l=0; l<1000000000; l++)
            //{

            //}
            //i = false;
            //t.Wait();

            DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
            Maze maze = dfsMaze.Generate(500, 500);
            Console.WriteLine(maze.ToString());
            IsearchableMaze ism = new IsearchableMaze(maze);
            BFS<Position> bfs = new BFS<Position>();
            Console.WriteLine(bfs.search(ism).numOfStates());
            DFS<Position> dfs = new DFS<Position>();
            Console.WriteLine(dfs.search(ism).numOfStates());

        }
    }
}
