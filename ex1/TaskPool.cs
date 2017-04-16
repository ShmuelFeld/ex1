using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class TaskPool
    {
        private List<Task> tasks;
        private bool stop;
        public TaskPool()
        {
            tasks = new List<Task>();
            stop = false;
            doTasks();
        }
        public void addTask(Task t) { tasks.Add(t); }
        public void doTasks()
        {
            while (!stop)
            {
                if (tasks.Any())
                {
                    Task t = tasks.First();
                    tasks.Remove(t);
                    t.Start();
                }
            }
        }
        public void terminate()
        {
            stop = true;
        }
    }
}
