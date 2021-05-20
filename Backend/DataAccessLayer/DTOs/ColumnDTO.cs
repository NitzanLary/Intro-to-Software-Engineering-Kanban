using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class ColumnDTO : DTO
    {
        public const string CreatorColumnName = "boardCreator";
        public const string BoardNameColumnName = "boardName";
        public const string MaxTasksNumberColumnName = "maxTasks";
        public const string ColumnOrdinalColumName = "columnOridnal";

        private string _boardname;
        public string Boardname { get => _boardname; set { _boardname = value; _controller.Update(_boardname, _creator, BoardNameColumnName, value); } }


        private string _creator;
        public string Creator { get => _creator; set { _creator = value; _controller.Update(_boardname, _creator, CreatorColumnName, value); } }


        private int _maxTasksNumber;
        public int MaxTasksNumber { get => _maxTasksNumber; set { _maxTasksNumber = value; _controller.Update(_boardname, _creator, MaxTasksNumberColumnName, value); } }

        private int _columnOrdinal;
        public int ColumnOrdinal { get => _columnOrdinal; }





        private List<TaskDTO> _tasks;
        public List<TaskDTO> Tasks { get => _tasks; set { _tasks = value; } }




        public ColumnDTO(string creator, string boardname, int columnOrdinal, int maxTasks, List<TaskDTO> tasks) : base(new ColumnDALController())
        {
            _creator = creator;
            _boardname = boardname;
            _columnOrdinal = columnOrdinal;
            _maxTasksNumber = maxTasks;
            _tasks = tasks;

        }
    }
}
