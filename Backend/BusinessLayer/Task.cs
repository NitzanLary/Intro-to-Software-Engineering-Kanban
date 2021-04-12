using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Task
    {
        private readonly int ID;
        private DateTime creationTime;
        public DateTime CreationTime
        {
            get => creationTime;
        }
        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set => dueDate = value;
        }
        private string title;
        public string Title
        {
            get => title;
            set => title = value;
        }
        private string description;
        public string Description
        {
            get => description;
            set => description = value;
        } 

        public Task(int ID, DateTime dueDate, string title, string description)
        {
            this.ID = ID;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
            this.creationTime = DateTime.Now;
        }

        

        public Response UpdateTaskDueDate(DateTime newDueDate)
        {
            DueDate = newDueDate;
            return new Response();
        }

        public Response UpdateTaskTitle(string newTitle)
        {
            Title = newTitle;
            return new Response();
        }

        public Response UpdateTaskDescription(string newDescription)
        {
            Description = newDescription;
            return new Response();
        }

    }
}
