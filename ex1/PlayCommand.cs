using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    enum Move {up, down, left, right}
    class PlayCommand
    {
        private IModel model;
        public PlayCommand(IModel model)
        {
            this.model = model;
        }
        //TODO
        public string Execute(string[] args)
        {
            //wroks?!
            Move move =  (Move)int.Parse(args[0]);
            Move m = model.play(move);
            return m.ToString();
        }
    }
}
