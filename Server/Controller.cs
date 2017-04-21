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
    /// <seealso cref="ex1.IController" />
    public class Controller : IController
    {

        /// <summary>
        /// The commands
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// The view
        /// </summary>
        private IView view;
        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void SetView(IView view) {
            this.view = view;
            commands["play"].SetView(view);
            commands["close"].SetView(view);
        }
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void SetModel(IModel model) { this.model = model; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            model = new Model(this);
            commands = new Dictionary<string, ICommand>();
            AddCommand("generate", new GenerateMazeCommand(model));
            AddCommand("list", new ListCommand(model));
            AddCommand("solve", new SolveGameCommand(model));
            AddCommand("start", new StartGameCommand(model));
            AddCommand("join", new JoinCommand(model));
            AddCommand("play", new PlayCommand(model));
            AddCommand("close", new CloseCommand(model));
        }
        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="command">The command.</param>
        public void AddCommand(string s, ICommand command)
        {
            commands.Add(s, command);
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }

}
