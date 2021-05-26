using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Column
    {
        private Dictionary<int, Task> tasks;
        public IList<Task> Tasks
        {
            get => tasks.Values.ToList();
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == null)
                    throw new ArgumentException("Column name can not be null");
                if (persisted)
                    //dto.Name = value;
                name = value;
            }
        }

        private ColumnDTO dto;
        public ColumnDTO DTO
        {
            get => dto;
            private set => dto = value;
        }

        private bool persisted;
        
        private int maxTasks;
        public int MaxTasks
        {
            get => maxTasks;
            set
            {
                if (value != -1 && value < Tasks.Count)
                    throw new ArgumentException("There are already more tasks in this column from the limit you put");
                if (persisted)
                    dto.MaxTasksNumber = value;
                maxTasks = value;
            }
        }

        public Column(string name)
        {
            persisted = false;
            Name = name;
            tasks = new Dictionary<int, Task>();
            maxTasks = -1;
        }

        public Column(ColumnDTO columnDTO)
        {
            //Name = columnDTO.Name;
            tasks = new Dictionary<int, Task>();
            MaxTasks = columnDTO.MaxTasksNumber;
            foreach (TaskDTO taskDTO in columnDTO.Tasks)
                tasks.Add(taskDTO.TaskID, new Task(taskDTO));
            persisted = true;
            dto = columnDTO;
        }

        public void AttachDto(string creator, string boardName, int columnOrdinal)
        {
            dto = new ColumnDTO(creator, boardName, columnOrdinal, MaxTasks, new List<TaskDTO>()); // TODO add name
            dto.Insert();
            persisted = true;
        }

        public Task AddTask(DateTime dueDate, string title, string description, string assignee)
        {
            Task task = new Task(dueDate, title, description, assignee);
            return AddTask(task);
        }

        internal Task AddTask(Task task)
        {
            if (MaxTasks != -1 && tasks.Count >= MaxTasks)
                throw new ArgumentException($"Max number of tasks allowed in this coloumn is {MaxTasks}");
            tasks.Add(task.ID, task);
            return task;
        }

        public void UpdateTaskDueDate(int taskId, DateTime date)
        {
            Update<DateTime>(taskId, (task) => task.DueDate = date);
        }

        public void UpdateTaskTitle(int taskId, String title)
        {
            Update<string>(taskId, (task) => task.Title = title);
        }

        public void UpdateTaskDescription(int taskId, String desc)
        {
            Update<string>(taskId, (task) => task.Description = desc);
        }

        public Task GetTask(int taskId)
        {
            return Update<Task>(taskId, (task) => task);
        }

        // A generic funtion to update a task's property
        public T Update<T>(int taskId, Func<Task,T> updateFunc)
        {
            Task task;
            if (!tasks.TryGetValue(taskId, out task))
                throw new ArgumentException($"Task ID: {taskId} not found");
            return updateFunc(task);
        }

        public void RemoveTask(Task task)
        {
            if (!tasks.ContainsKey(task.ID))
                throw new ArgumentException($"Task ID: {task.ID} not found");
            tasks.Remove(task.ID);
        }

    }
}
