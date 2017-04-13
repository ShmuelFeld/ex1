using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    interface IModel
    {
        Maze generateMaze(string name, int rows, int cols);
        Solution<Position> solveMaze(string name,int  algorithm);
        Maze startGame(string name, int rows, int cols);
        List<Maze> getListOfAvailableGames();
        Maze join(string name);
        Move play(Move move);
        //close
    }
}
