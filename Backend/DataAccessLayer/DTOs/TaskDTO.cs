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
        public const string CreatorColumnName = "BoardCreator";
        public const string BoardNameColumnName = "BoardName";
        public const string ColumnColumnName = "Column";
        public const string IdColumnName = "id";
        public const string CreationTimeColumnName = "CreationTime";
        public const string dueDateColumnName = "DueDate";
        public const string TitleColumnName = "Title";
        public const string DescriptionColumnName = "Description";
        public const string AsigneeColumnName = "Asignee";
        public const string StatusColumnName = "status";
        public const string TasksNumberColumnName = "tasksNumber";

        private string _boardname;
        public string Boardname { get => _boardname; set { _boardname = value; _controller.update(_boardname, _creator, BoardNameColumnName, value); } }


        private string _creator;
        public string Creator { get => _creator; set { _creator = value; _creator.update(_boardname, _creator, CreatorColumnName, value); } }


        private int _column;
        public int _column { get => _column; set { _column = value; _column.update(_boardname, _creator, ColumnColumnName, value); } }


        private int _maxDesc;
        public int MaxInProgress { get => _maxDesc; set { _maxDesc = value; _maxDesc.update(_boardname, _creator, MaxDescColumnName, value); } }


        private int _tasksNumber;
        public int MaxInDone { get => _tasksNumber; set { _tasksNumber = value; _tasksNumber.update(_boardname, _creator, TasksNumberColumnName, value); } }






        public ColumnDTO(string creator, string boardname, int maxTitle, int maxDesc, int tasksNumber) : base(new BoardDALController())
        {
            _boardname = boardname;
            _creator = creator;
            _maxTitle = maxTitle;
            _maxDesc = maxDesc;
            _tasksNumber = tasksNumber;
        }
    }
}
