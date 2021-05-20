using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDTO : DTO
    {
        public const string BoardNameColumnName = "boardName";
        public const string CreatorColumnName = "boardCreator";
        public const string ColumnOrdinalColumnName = "columnOrdinal";
        public const string IdColumnName = "taskID";
        public const string CreationTimeColumnName = "creationTime";
        public const string dueDateColumnName = "dueDate";
        public const string TitleColumnName = "title";
        public const string DescriptionColumnName = "description";
        public const string AssigneeColumnName = "assignee";

        private string _boardname;
        public string Boardname { get => _boardname; set { _boardname = value; _controller.Update(_boardname, _creator, BoardNameColumnName, value); } }


        private string _creator;
        public string Creator { get => _creator; set { _creator = value; _controller.Update(_boardname, _creator, CreatorColumnName, value); } }


        private int _columnOrdinal;
        public int ColumnOrdinal { get => _columnOrdinal; set { _columnOrdinal = value; _controller.Update(_boardname, _creator, ColumnOrdinalColumnName, value); } }


        private int _taskID;
        public int TaskID { get => _taskID; set { _taskID = value; _controller.Update(_boardname, _creator, IdColumnName, value); } }


        private string _title;
        public string Title { get => _title; set { _controller.Update(_boardname, _creator, TitleColumnName, value); _title = value; } }


        private string _description;
        public string Description { get => _description; set { _controller.Update(_boardname, _creator,DescriptionColumnName, value); _description = value; } }


        private string _assignee;
        public string Assignee { get => _assignee; set { _assignee = value; _controller.Update(_boardname, _creator, AssigneeColumnName, value); } }

        private string _dueTime;
        public string DueTime { get => _dueTime; set { _dueTime = value; _controller.Update(_boardname, _creator, _dueTime, value); } }


        private string _creationTime;
        public string CreationTime { get => _creationTime; set { _creationTime = value; _controller.Update(_boardname, _creator, _creationTime, value); } }



        public TaskDTO(string boardName, string boardCreator, int columnOrdinal, int taskID, string title, 
                        string description, string assignee, string dueTime, string creationTime) : base(new TaskDALController())
        {
            _boardname = boardName;
            _creator = boardCreator;
            _columnOrdinal = columnOrdinal;
            _taskID = taskID;
            _title = title;
            _description = description;
            _assignee = assignee;
            _dueTime = dueTime;
            _creationTime = creationTime;
            
        }
    }
}
