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
    public class ListCommand : ICommand
    {
        private IModel model;
        public ListCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            List<Maze> availableGamesList = model.getListOfAvailableGames();
            return ToJSON(availableGamesList);
        }

        public void setView(IView v)
        {
            throw new NotImplementedException();
        }

        private string ToJSON(List<Maze> list)
        {
            JArray listArr = new JArray();
            foreach (Maze m in list)
            {
                listArr.Add(m.Name);
            }
            return listArr.ToString();
        }
    }
}
