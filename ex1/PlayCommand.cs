using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class PlayCommand : ICommand
    {
        private IModel model;
        private IView view;
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        public void setView(IView v) { view = v; }
        public string Execute(string[] args, TcpClient client)
        {
            string move =  args[0];
            //string moveString = null;
            //if (move == Direction.Up) { moveString = "up"; }
            //else if (move == Direction.Down) { moveString = "down"; }
            //else if (move == Direction.Right) { moveString = "right"; }
            //else if (move == Direction.Left) { moveString = "left"; }
            //else { return "invalid move"; }
            if((move != "up") && (move != "down") && (move != "right") && (move != "left")) { return "invalid move"; }
            MultiPlayerGame game = model.play(move, client);
            TcpClient tcpOfOtherClient = null;
            if (game.FirstPlayer == client) {
                tcpOfOtherClient = game.getSecondPlayer();
            } 
            else if (game.getSecondPlayer() == client)
            {
                tcpOfOtherClient = game.FirstPlayer;
            }
            view.sendToOtherClient(ToJSON(game.getMazeName(), move), tcpOfOtherClient);
            return "other player has been notified";
        }
        public string ToJSON(string name, string move)
        {
            JObject moveObj = new JObject();
            moveObj["Name"] = name;
            moveObj["Direction"] = move;
            return moveObj.ToString();
        }
    }
}
