using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
namespace ex1
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ex1.ISearchable{MazeLib.Position}" />
    class IsearchableMaze : ISearchable<Position>
    {
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// The initial position
        /// </summary>
        private Position initialPosition;
        /// <summary>
        /// The goal position
        /// </summary>
        private Position goalPosition;
        /// <summary>
        /// Initializes a new instance of the <see cref="IsearchableMaze"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public IsearchableMaze(Maze maze)
        {
            this.maze = maze;
            this.initialPosition = maze.InitialPos;
            this.goalPosition = maze.GoalPos;
        }
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public Dictionary<State<Position>, double> getAllPossibleStates(State<Position> s)
        {
            int col = s.Instance.Col;
            int row = s.Instance.Row;
            Dictionary<State<Position>, double> neighbors = new Dictionary<State<Position>, double>();
            Position left = new Position(row, col - 1);
            if (isValid(left))
            {
                neighbors.Add(new State<Position>(left), 1);
            }
            Position up = new Position(row - 1, col);
            if (isValid(up))
            {
                neighbors.Add(new State<Position>(up), 1);
            }
            Position right = new Position(row, col + 1);
            if (isValid(right))
            {
                neighbors.Add(new State<Position>(right), 1);
            }
            Position down = new Position(row + 1, col);
            if (isValid(down))
            {
                neighbors.Add(new State<Position>(down), 1);
            }
            return neighbors;
        }
        /// <summary>
        /// Determines whether the specified p is valid.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>
        ///   <c>true</c> if the specified p is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool isValid(Position p)
        {
            if (p.Col >= this.maze.Cols || p.Col < 0) { return false; }
            if (p.Row >= this.maze.Rows || p.Row < 0) { return false; }
            if (maze[p.Row,p.Col] == CellType.Wall) { return false; }
            return true;
        }
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        State<Position> ISearchable<Position>.getGoalState()
        {
            return new State<Position>(this.goalPosition);
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<Position> ISearchable<Position>.getInitialState()
        {
            return new State<Position>(this.initialPosition);
        }
    }
}
