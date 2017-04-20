using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class CloseCommand : ICommand
    {
        private IModel model;
        public CloseCommand (IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            throw new NotImplementedException();
        }

        public void setView(IView v) { view = v; }
    }
}
