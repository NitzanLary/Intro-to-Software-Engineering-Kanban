using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class ColumnDTO: DTO
    {
        public const string CreatorColumnName = "CreatorColumnName";
        public const string BoardNameColumnName = "BoardName";
        public const string MaxTitleColumnName = "MaxTitle";
        public const string MaxDescColumnName = "MaxDesc";
        public const string TasksNumberColumnName = "tasksNumber";

        private string _boardname;
        public string Boardname { get => _boardname; set { _boardname = value; _controller.update(_boardname, _creator, BoardNameColumnName, value); } }


        private string _creator;
        public string Creator { get => _creator; set { _creator = value; _creator.update(_boardname, _creator, CreatorColumnName, value); } }


        private int _maxTitle;
        public int MaxBackLog { get => _maxTitle; set { _maxTitle = value; _maxTitle.update(_boardname, _creator, MaxTitleColumnName, value); } }


        private int _maxDesc;
        public int MaxInProgress { get => _maxDesc; set { _maxDesc = value; _maxDesc.update(_boardname, _creator, MaxDescColumnName, value); } }


        private int _tasksNumber;
        public int MaxInDone { get => _tasksNumber; set { _tasksNumber = value; _tasksNumber.update(_boardname, _creator, TasksNumberColumnName, value); } }


        private List<TaskDTO> _tasks;
        public List<TaskDTO> Tasks { get => _tasks; }




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
