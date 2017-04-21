using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiPlayerGame
    {
        /// <summary>
        /// Gets or sets the first player.
        /// </summary>
        /// <value>
        /// The first player.
        /// </value>
        public TcpClient FirstPlayer { get; set; }
        /// <summary>
        /// The second player
        /// </summary>
        public TcpClient SecondPlayer { get; set; }
        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze { get; set; }
        /// <summary>
        /// The is joined
        /// </summary>
        private bool isJoined;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerGame"/> class.
        /// </summary>
        public MultiPlayerGame()
        {            
            isJoined = false;
        }
        /// <summary>
        /// Gets the second player.
        /// </summary>
        /// <returns></returns>
        public TcpClient GetSecondPlayer() { return SecondPlayer; }
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="player1">The player1.</param>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        public bool StartGame(TcpClient player1, Maze m)
        {
            Maze = m;
            FirstPlayer = player1;
            while (!isJoined) { }
            return true;
        }
        /// <summary>
        /// Joins the specified player2.
        /// </summary>
        /// <param name="player2">The player2.</param>
        public void join(TcpClient player2)
        {
            SecondPlayer = player2;
            isJoined = true;
        }
        /// <summary>
        /// Gets the name of the maze.
        /// </summary>
        /// <returns></returns>
        public string GetMazeName() { return Maze.Name; }
    }
}
