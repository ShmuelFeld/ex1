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
    class ListCommand : ICommand
    {
        private IModel model;
        public ListCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            List<Maze> availableGamesList = model.getListOfAvailableGames();
            //TODO check if it's done by itself
            return ToJSON(availableGamesList);
        }
        private string ToJSON(List<Maze> list)
        {
            JObject listObj = new JObject();
            JArray listArr = new JArray();
            foreach (Maze m in list)
            {
                listArr.Add(m);
            }
            listObj["x"] = listArr;
            return listObj.ToString();
        }
    }
}
