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
        public readonly string name;
        public readonly string boardName;
        public readonly string creator;
        public readonly int columnOrdinal;

        internal Column(List<Task> tasks, int maxTasks, string name)
        {
            this.tasks = tasks;
            this.maxTasks = maxTasks;
            this.name = name;
            columnOrdinal = -1;
            creator = null;
            boardName = null;
        }

        internal Column(BusinessLayer.Column column)
        {
            this.tasks = column.Tasks.Select(t => new Task(t)).ToList();
            this.maxTasks = column.MaxTasks;
            this.name = column.Name;
            columnOrdinal = column.DTO.ColumnOrdinal;
            creator = column.DTO.Creator;
            boardName = column.DTO.Boardname;
        }
    }
}
