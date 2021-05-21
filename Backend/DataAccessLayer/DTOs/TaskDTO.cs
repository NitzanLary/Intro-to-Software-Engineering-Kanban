using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO : DTO
    {
        public const string BoardNameColumnName = "boardName";
        public const string CreatorColumnName = "boardCreator";
        public const string ColumnOrdinalColumnName = "columnOrdinal";
        public const string IdColumnName = "taskID";
        public const string CreationTimeColumnName = "creationTime";
        public const string dueDateColumnName = "dueTime";
        public const string TitleColumnName = "title";
        public const string DescriptionColumnName = "description";
        public const string AssigneeColumnName = "assignee";

        private string _boardname;
        public string Boardname { get => _boardname; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, BoardNameColumnName, value)) { _boardname = value; } } }


        private string _creator;
        public string Creator { get => _creator; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, CreatorColumnName, value)) { _creator = value; } } }


        private int _columnOrdinal;
        public int ColumnOrdinal { get => _columnOrdinal; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, ColumnOrdinalColumnName, value)) { _columnOrdinal = value; } } }


        private int _taskID;
        public int TaskID { get => _taskID; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, IdColumnName, value)) { _taskID = value; } } }


        private string _title;
        public string Title { get => _title; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, TitleColumnName, value)) { _title = value; } } }


        private string _description;
        public string Description { get => _description; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, DescriptionColumnName, value)) { _description = value; } } }


        private string _assignee;
        public string Assignee { get => _assignee; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, AssigneeColumnName, value)) { _assignee = value; } } }

        private string _dueTime;
        public string DueTime { get => _dueTime; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, _dueTime, value)) { _dueTime = value; } } }


        private string _creationTime;
        public string CreationTime { get => _creationTime; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _taskID, IdColumnName, _creationTime, value)) { _creationTime = value; } } }



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

        public bool Insert()
        {
            return _controller.Insert(this);
        }

    }
}
