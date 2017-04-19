using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class MultiPlayerGame
    {
        public TcpClient FirstPlayer { get; set; }
        public TcpClient secondPlayer;// { get; set; }
        public Maze maze { get; set; }
        private bool isJoined;
        public MultiPlayerGame()
        {            
            isJoined = false;
        }
        public TcpClient getSecondPlayer() { return secondPlayer; }
        public bool startGame(TcpClient player1, Maze m)
        {
            maze = m;
            FirstPlayer = player1;
            while (!isJoined) { }
            return true;
        }
        public void join(TcpClient player2)
        {
            secondPlayer = player2;
            isJoined = true;
        }
        public string getMazeName() { return maze.Name; }
    }
}
