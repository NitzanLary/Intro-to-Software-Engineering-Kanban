using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Column
    {
        private Dictionary<int, Task> tasks;
        public List<Task> Tasks
        {
            get => tasks.Values.ToList();
        }
        //private ColumnDTO dto;
        private int maxTasks;
        public int MaxTasks
        {
            get => maxTasks;
            set
            {
                //if (persisted)
                //    dto.MaxTasks = value;
                maxTasks = value;
            }
        }

        public Column()
        {
            tasks = new Dictionary<int, Task>();
            MaxTasks = -1;
        }

        public Task addTask(DateTime dueDate, string title, string description, string assignee)
        {
            if (tasks.Count >= MaxTasks)
                throw new ArgumentOutOfRangeException($"Max number of tasks allowed in this coloumn is {MaxTasks}");
            Task task = new Task(dueDate, title, description, assignee);
            tasks.Add(task.ID, task);
            return task;
        }

        public void updateTaskDueDate(int taskId, DateTime date)
        {
            update<DateTime>(taskId, (task) => task.DueDate = date);
        }

        public void updateTaskTitle(int taskId, String title)
        {
            update<string>(taskId, (task) => task.Title = title);
        }

        public void updateTaskDescription(int taskId, String desc)
        {
            update<string>(taskId, (task) => task.Description = desc);
        }

        public Task GetTask(int taskId)
        {
            return update<Task>(taskId, (task) => task);
        }

        // A generic funtion to update a task's property
        public T update<T>(int taskId, Func<Task,T> updateFunc)
        {
            Task task;
            if (!tasks.TryGetValue(taskId, out task))
                throw new ArgumentException($"Task ID: {taskId} not found");
            return updateFunc(task);
        }



    }
}
