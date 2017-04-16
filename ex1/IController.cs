using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public interface IController
    {
        void setModel(IModel model);
        void setView(IView view);
        void addCommand(string s, ICommand command);
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}
