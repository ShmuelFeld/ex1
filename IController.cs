using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IController
    {
        void setModel(IModel model);
        void setView(IView view);
        void addCommand(string s, ICommand command);
        string ExecuteCommand(string commandLine);
    }
}
