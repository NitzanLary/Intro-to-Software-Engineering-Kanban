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
            set => creationTime = value;

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

        Task(int ID, DateTime creationTime, string title, string description)
        {
            this.ID = ID;
            this.creationTime = creationTime;
            this.title = title;
            this.description = description;
        }

        

        public Response UpdateTaskDueDate(DateTime newDueDate)
        {
            throw new NotImplementedException();
        }

        public Response UpdateTaskTitle(string newTitle)
        {
            throw new NotImplementedException();
        }

        public Response UpdateTaskDescription(string newDescription)
        {
            throw new NotImplementedException();
        }

    }
}
