using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using System.Net.Sockets;

namespace ex1
{
    public class Model : IModel
    {
        IController controller;        
        Dictionary<string, Maze> mazes;
        Dictionary<string, Solution<Position>> BFSsoliutions;
        Dictionary<string, Solution<Position>> DFSsoliutions;
        Dictionary<string, TcpClient> waitingGames;
        List<Maze> availableGames;
        //private TaskPool taskPool;

        public Model(IController controller)
        {
            availableGames = new List<Maze>();
            //taskPool = new TaskPool();
            mazes = new Dictionary<string, Maze>();
            this.controller = controller;
            BFSsoliutions = new Dictionary<string, Solution<Position>>();
            DFSsoliutions = new Dictionary<string, Solution<Position>>();
            waitingGames = new Dictionary<string, TcpClient>();
        }

        public Maze generateMaze(string name, int rows, int cols)
        {
            if (mazes.ContainsKey(name))
            {
                return null;
            }
            Task<Maze> t = new Task<Maze>(() => {
                DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
                Maze m = dfsMaze.Generate(rows, cols);
                m.Name = name;
                mazes.Add(name, m);
                return m;
            });
            t.Start();
            Maze maze = t.Result;
            IsearchableMaze ism = new IsearchableMaze(maze);
            Task BFSsolutionTask = new Task(() =>
            {
                BFS<Position> bfs = new BFS<Position>();
                BFSsoliutions.Add(maze.Name, bfs.search(ism));
            });
            BFSsolutionTask.Start();
            Task DFSsolutionTask = new Task(() =>
            {
                DFS<Position> dfs = new DFS<Position>();
                DFSsoliutions.Add(maze.Name, dfs.search(ism));
            });
            DFSsolutionTask.Start();
            //taskPool.addTask(t);
            return maze;
        }

        public MazeSolution solveMaze(string name, int algorithm)
        {           
            if (!mazes.ContainsKey(name))
            {
                return null;
            }
            //BFS
            if (algorithm == 0)
            {
                return BFSsoliutions[name] as MazeSolution; //todo
            }
            //DFS
            else if (algorithm == 1)
            {
                return DFSsoliutions[name] as MazeSolution; //todo
            }
            return null; 
        }

        public Maze startGame(string name, int rows, int cols, TcpClient tcpClient)
        {
            if (mazes.ContainsKey(name))
            {
                return null;
            }
            DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
            Maze m = dfsMaze.Generate(rows, cols);
            m.Name = name;
            waitingGames.Add(name, tcpClient);
            availableGames.Add(m);
            return m;
        }
        public List<Maze> getListOfAvailableGames()
        {
            return availableGames;
        }

        public Maze join(string name)
        {
            if (!waitingGames.ContainsKey(name)) { return null; }
            waitingGames.Remove(name);
            return mazes[name];
        }

        public Move play(Move move)
        {
            //??
            throw new NotImplementedException();
        }
        
    }
}
