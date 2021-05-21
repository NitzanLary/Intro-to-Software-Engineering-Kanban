using IntroSE.Kanban.Backend.DataAccessLayer;
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
        private static int indexer = 0;
        readonly private int MAX_TITLE = 50;
        readonly private int MAX_DESC = 300;
        private bool persisted;

        private readonly int id;
        public int ID
        {
            get => id;
        }

        private readonly DateTime creationTime;
        public DateTime CreationTime
        {
            get => creationTime;
        }
        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("Invalid Date");
                //if (persisted)
                //    dto.DueDate = value;
                dueDate = value;
            }
        }
        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (value == null || value.Length > MAX_TITLE || value.Length == 0)
                    throw new ArgumentException($"Title Must Be Between 1 To {MAX_TITLE} Characters");
                if (persisted)
                    dto.Title = value;
                title = value;
            }
        }
        private string description;
        public string Description
        {
            get => description;
            set
            {
                if (value == null || value.Length > MAX_DESC)
                    throw new ArgumentException($"Description Must Be Between 1 To {MAX_DESC} Characters");
                if (persisted)
                    dto.Description = value;
                description = value;
            }
        }

        private string assignee;
        public string Assignee
        {
            get => assignee;
            set
            {
                if (persisted)
                    dto.Assignee = value;
                assignee = value;
            }
            
        }

        private TaskDTO dto;
        private TaskDTO DTO
        {
            set
            {
                dto = value;
            }
        }

        public Task(DateTime dueDate, string title, string description, string assignee)
        {
            id = indexer++;
            DueDate = dueDate;
            Title = title;
            Description = description;
            Assignee = assignee;
            persisted = false;
            // TODO: creating DTO object?
        }

        public Task(TaskDTO taskDTO)
        {
            id = taskDTO.TaskID;
            //DueDate = taskDTO.DueTime;
            Title = taskDTO.Title;
            Description = taskDTO.Description;
            Assignee = taskDTO.Assignee;
            persisted = true;
            dto = taskDTO;
        }

    }
}
