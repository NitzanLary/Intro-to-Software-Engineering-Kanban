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
        public const string ColumnOrdinalColumnName = "column";
        public const string IdColumnName = "id";
        public const string CreationTimeColumnName = "creationTime";
        public const string dueDateColumnName = "dueDate";
        public const string TitleColumnName = "title";
        public const string DescriptionColumnName = "description";
        public const string AsigneeColumnName = "asignee";
        public const string StatusColumnName = "status";
        public const string TasksNumberColumnName = "tasksNumber";

        private string _boardname;
        public string Boardname { get => _boardname; set { _boardname = value; _controller.update(_boardname, _creator, BoardNameColumnName, value); } }


        private string _creator;
        public string Creator { get => _creator; set { _creator = value; _creator.update(_boardname, _creator, CreatorColumnName, value); } }


        private int _columnOrdinal;
        public int ColumnOrdinal { get => _columnOrdinal; set { _columnOrdinal = value; _controller.update(_boardname, _creator, ColumnOrdinalColumnName, value); } }

        private string _title;
        public string Title { get => _title; set { _controller.Update(_boardname, _creator, TitleColumnName, value; _title = value; } }

        private string _description;
        public string Description { get => _description; set { _controller.Update(_boardname, _creator,DescriptionColumnName, value); _description = value; } }

        private int _maxDesc;
        public int MaxInProgress { get => _maxDesc; set { _maxDesc = value; _maxDesc.update(_boardname, _creator, MaxDescColumnName, value); } }


        private int _tasksNumber;
        public int MaxInDone { get => _tasksNumber; set { _tasksNumber = value; _tasksNumber.update(_boardname, _creator, TasksNumberColumnName, value); } }






        public ColumnDTO(string creator, string boardname, int maxTitle, int maxDesc, int tasksNumber) : base(new ColumnDALController())
        {
            _boardname = boardname;
            _creator = creator;
            _maxTitle = maxTitle;
            _maxDesc = maxDesc;
            _tasksNumber = tasksNumber;
        }
    }
}
