using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IView view;
        public void setView(IView view) {
            this.view = view;
            commands["play"].setView(view);
        }
        public void setModel(IModel model) { this.model = model; }
        public Controller()
        {
            model = new Model(this);
            commands = new Dictionary<string, ICommand>();
            addCommand("generate", new GenerateMazeCommand(model));
            addCommand("list", new ListCommand(model));
            addCommand("solve", new SolveGameCommand(model));
            addCommand("start", new StartGameCommand(model));
            addCommand("join", new JoinCommand(model));
            addCommand("play", new PlayCommand(model));
        }
        public void addCommand(string s, ICommand command)
        {
            commands.Add(s, command);
        }
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
