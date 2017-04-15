using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IView view;
        public void setView(IView view) { this.view = view; }
        public void setModel(IModel model) { this.model = model; }
        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
        }
        public void addCommand(string s, ICommand command)
        {
            commands.Add(s, command);
        }
        public string ExecuteCommand(string commandLine)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, null);
        }
    }
}
