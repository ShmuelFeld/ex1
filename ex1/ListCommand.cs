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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ex1.ICommand" />
    public class ListCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            List<Maze> availableGamesList = model.getListOfAvailableGames();
            return ToJSON(availableGamesList);
        }

        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void setView(IView v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
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
