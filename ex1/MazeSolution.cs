using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    public class MazeSolution
    {
        private Stack<State<Position>> backTrace;
        public MazeSolution(Stack<State<Position>> bt)
        {
            backTrace = bt;
        }
        public new string ToString()
        {
            string solution = "";
            Stack<State<Position>> temp = backTrace;
            State<Position> prev = temp.Pop();
            while (temp.Any())
            {
                State<Position> cur = temp.Pop();
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
                prev = cur;
            }
            return solution;
        }

        public string ToJSON(string name)
        {
            JObject solveObj = new JObject();
            solveObj["Name"] = name;
            solveObj["solution"] = ToString();
            solveObj["NodesEvaluated"] = backTrace.Count();
            return solveObj.ToString();
        }
    }
}
