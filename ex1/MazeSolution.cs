using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class MazeSolution : Solution<Position>
    {
        public string ToJSON(string s) { return null; }
        public new string ToString()
        {
            string solution = "";
            Stack<State<Position>> temp = backTrace;
            State<Position> prev = temp.First();
            while (!temp.Any())
            {
                State<Position> cur = temp.First();
                int pRow = prev.Instance.Row;
                int pCol = prev.Instance.Col;
                int cRow = cur.Instance.Row;
                int cCol = cur.Instance.Col;
                //left
                if (pRow < cRow)
                {
                    solution += "0";
                }
                //right
                if (pRow > cRow)
                {
                    solution += "1";
                }
                //up
                if (pCol < cCol)
                {
                    solution += "2";
                }
                //down
                if (pCol > cCol)
                {
                    solution += "3";
                }
            }
            return solution;
        }
    }
}
