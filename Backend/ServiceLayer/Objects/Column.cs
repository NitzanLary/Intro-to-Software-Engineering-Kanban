using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    public struct Column
    {
        public readonly List<Task> tasks;
        public readonly int maxTasks;
        internal Column(List<Task> tasks, int maxTasks)
        {
            this.tasks = tasks;
            this.maxTasks = maxTasks;
        }
    }
}
