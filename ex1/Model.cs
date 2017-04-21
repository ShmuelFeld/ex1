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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ex1.IModel" />
    public class Model : IModel
    {
        /// <summary>
        /// The controller
        /// </summary>
        IController controller;
        /// <summary>
        /// The mazes
        /// </summary>
        Dictionary<string, Maze> mazes;
        /// <summary>
        /// The bf ssoliutions
        /// </summary>
        Dictionary<string, Solution<Position>> bfsSoliutions;
        /// <summary>
        /// The df ssoliutions
        /// </summary>
        Dictionary<string, Solution<Position>> dfsSoliutions;
        /// <summary>
        /// The waiting games
        /// </summary>
        Dictionary<string, TcpClient> waitingGames;
        /// <summary>
        /// The multi player games
        /// </summary>
        Dictionary<string, MultiPlayerGame> multiPlayerGames;
        /// <summary>
        /// The available games
        /// </summary>
        List<Maze> availableGames;
        //private TaskPool taskPool;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public Model(IController controller)
        {
            availableGames = new List<Maze>();
            //taskPool = new TaskPool();
            mazes = new Dictionary<string, Maze>();
            this.controller = controller;
            bfsSoliutions = new Dictionary<string, Solution<Position>>();
            dfsSoliutions = new Dictionary<string, Solution<Position>>();
            waitingGames = new Dictionary<string, TcpClient>();
            multiPlayerGames = new Dictionary<string, MultiPlayerGame>();
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
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
            Task bfsSolutionTask = new Task(() =>
            {
                BFS<Position> bfs = new BFS<Position>();
                bfsSoliutions.Add(maze.Name, bfs.Search(ism));
            });
            bfsSolutionTask.Start();
            Task dfsSolutionTask = new Task(() =>
            {
                DFS<Position> dfs = new DFS<Position>();
                dfsSoliutions.Add(maze.Name, dfs.Search(ism));
            });
            dfsSolutionTask.Start();
            return maze;
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        public MazeSolution SolveMaze(string name, int algorithm)
        {           
            if (!mazes.ContainsKey(name))
            {
                return null;
            }
            //BFS
            if (algorithm == 0)
            {                
                MazeSolution bfsSolustion = new MazeSolution(bfsSoliutions[name].BackTrace,
                    bfsSoliutions[name].EvaluatedNodes);
                return bfsSolustion; 
            }
            //DFS
            else if (algorithm == 1)
            {
                MazeSolution dfsSolustion = new MazeSolution(dfsSoliutions[name].BackTrace,
                    dfsSoliutions[name].EvaluatedNodes);
                return dfsSolustion;
            }
            return null; 
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="tcpClient">The TCP client.</param>
        /// <returns></returns>
        public Maze StartGame(string name, int rows, int cols, TcpClient tcpClient)
        {
            Maze maze;
            if (mazes.ContainsKey(name))
            {
                return null;
            }
            else
            {
                maze = GenerateMaze(name, rows, cols);
            }
            availableGames.Add(maze);
            waitingGames.Add(name, tcpClient);
            MultiPlayerGame multiPlayerGame = new MultiPlayerGame();
            multiPlayerGames.Add(name, multiPlayerGame);
            multiPlayerGame.StartGame(tcpClient, maze);            
            return maze;
        }
        /// <summary>
        /// Gets the list of available games.
        /// </summary>
        /// <returns></returns>
        public List<Maze> GetListOfAvailableGames()
        {
            return availableGames;
        }

        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tcpClient">The TCP client.</param>
        /// <returns></returns>
        public Maze Join(string name, TcpClient tcpClient)
        {
            if (!waitingGames.ContainsKey(name)) { return null; }
            Maze maze = mazes[name];
            waitingGames.Remove(name);
            availableGames.Remove(maze);
            MultiPlayerGame multiPlayer = multiPlayerGames[name];
            multiPlayer.join(tcpClient);
            return mazes[name];
        }

        /// <summary>
        /// Plays the specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public MultiPlayerGame Play(string move, TcpClient client)
        {
            foreach (MultiPlayerGame m in multiPlayerGames.Values)
            {
                if ((client == m.FirstPlayer) || (client == m.GetSecondPlayer())) { return m; }
            }
            return null;
        }
        /// <summary>
        /// Closes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public TcpClient Close (TcpClient client)
        {
            foreach (MultiPlayerGame m in multiPlayerGames.Values)
            {
                if(client == m.FirstPlayer)
                {
                    multiPlayerGames.Remove(m.GetMazeName());
                    return m.GetSecondPlayer();
                }
                else if (client == m.GetSecondPlayer())
                {
                    multiPlayerGames.Remove(m.GetMazeName());
                    return m.FirstPlayer;
                }
            }
            return null;
        }
        
    }
}
