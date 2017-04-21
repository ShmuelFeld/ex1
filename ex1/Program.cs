using MazeGeneratorLib;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
            Maze maze = dfsMaze.Generate(300, 300);
            Console.WriteLine(maze.ToString());
            IsearchableMaze ism = new IsearchableMaze(maze);
            BFS<Position> bfs = new BFS<Position>();
            bfs.search(ism).numOfStates();
            DFS<Position> dfs = new DFS<Position>();
            IsearchableMaze ism2 = new IsearchableMaze(maze);
            dfs.search(ism2).numOfStates();
        }
    }
}
