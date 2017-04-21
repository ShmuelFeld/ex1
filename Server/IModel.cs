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
    public interface IModel
    {
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        Maze generateMaze(string name, int rows, int cols);
        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        MazeSolution solveMaze(string name,int  algorithm);
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="tcp">The TCP.</param>
        /// <returns></returns>
        Maze startGame(string name, int rows, int cols, TcpClient tcp);
        /// <summary>
        /// Gets the list of available games.
        /// </summary>
        /// <returns></returns>
        List<Maze> getListOfAvailableGames();
        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tcp">The TCP.</param>
        /// <returns></returns>
        Maze join(string name, TcpClient tcp);
        /// <summary>
        /// Plays the specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        MultiPlayerGame play(string move, TcpClient client);
        /// <summary>
        /// Closes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        TcpClient close(TcpClient client);
    }
}
