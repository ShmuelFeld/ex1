using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;

namespace ex1
{
    class Model : IModel
    {
        IController controller;
        //DO WE NEED BOTH?
        List<Maze> availableGames;
        Dictionary<string, Maze> mazes;

        public Model(IController controller)
        {
            availableGames = new List<Maze>();
            mazes = new Dictionary<string, Maze>();
            this.controller = controller;
        }

        public Maze generateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator dfsMaze = new DFSMazeGenerator();
            Maze maze = dfsMaze.Generate(rows, cols);
            maze.Name = name;
            mazes.Add(name, maze);
            availableGames.Add(maze); //make the maze an available game
            return maze;
        }

        public Solution<Position> solveMaze(string name, int algorithm)
        {
            if (!mazes.ContainsKey(name))
            {
                return null;
            }
            Maze maze = mazes[name];
            if(algorithm == 0)
            {
                BFS<Position> bfs = new BFS<Position>();
                IsearchableMaze ism = new IsearchableMaze(maze);
                return bfs.search(ism);
            }
            else if (algorithm == 1)
            {
                DFS<Position> dfs = new DFS<Position>();
                IsearchableMaze ism = new IsearchableMaze(maze);
                return dfs.search(ism);
            } else { return null; }
        }

        public Maze startGame(string name, int rows, int cols)
        {
            //??
            throw new NotImplementedException();
        }
        public List<Maze> getListOfAvailableGames()
        {
            return availableGames;
        }

        public Maze join(string name)
        {
            //TODO SHMUEL?
            Maze maze = mazes[name];
            availableGames.Remove(maze);
            return maze;
        }

        public Move play(Move move)
        {
            //??
            throw new NotImplementedException();
        }
        
    }
}
