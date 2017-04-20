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
        Dictionary<string, MultiPlayerGame> multiPlayerGames;
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
            multiPlayerGames = new Dictionary<string, MultiPlayerGame>();
        }

        public Maze generateMaze(string name, int rows, int cols)
        {
            if (mazes.ContainsKey(name))
            {
                return mazes[name];
            }

            DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
            Maze maze = dfsMaze.Generate(rows, cols);
            maze.Name = name;
            mazes.Add(name, maze);
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
                MazeSolution bfsSolustion = new MazeSolution(BFSsoliutions[name].getBackTrace());
                return bfsSolustion; 
            }
            //DFS
            else if (algorithm == 1)
            {
                MazeSolution dfsSolustion = new MazeSolution(DFSsoliutions[name].getBackTrace());
                return dfsSolustion;
            }
            return null; 
        }

        public Maze startGame(string name, int rows, int cols, TcpClient tcpClient)
        {
            Maze maze;
            if (mazes.ContainsKey(name))
            {
                return null;
            }
            else
            {
                maze = generateMaze(name, rows, cols);
            }
            availableGames.Add(maze);
            waitingGames.Add(name, tcpClient);
            MultiPlayerGame multiPlayerGame = new MultiPlayerGame();
            multiPlayerGames.Add(name, multiPlayerGame);
            multiPlayerGame.startGame(tcpClient, maze);            
            return maze;
        }
        public List<Maze> getListOfAvailableGames()
        {
            return availableGames;
        }

        public Maze join(string name, TcpClient tcpClient)
        {
            if (!waitingGames.ContainsKey(name)) { return null; }
            Maze maze = mazes[name];
            waitingGames.Remove(name);
            availableGames.Remove(maze);
            MultiPlayerGame multiPlayer = multiPlayerGames[name];
            multiPlayer.join(tcpClient);
            return mazes[name];
        }

        public MultiPlayerGame play(string move, TcpClient client)
        {
            foreach (MultiPlayerGame m in multiPlayerGames.Values)
            {
                if ((client == m.FirstPlayer) || (client == m.getSecondPlayer())) { return m; }
            }
            return null;
        }
        public TcpClient close (TcpClient client)
        {
            foreach (MultiPlayerGame m in multiPlayerGames.Values)
            {
                if(client == m.FirstPlayer) { return m.getSecondPlayer(); }
                else if (client == m.getSecondPlayer()) { return m.FirstPlayer; }
                multiPlayerGames.Remove(m.getMazeName());
            }
            return null;
        }
        
    }
}
