using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnDTO : DTO
    {
        public const string CreatorColumnName = "boardCreator";
        public const string BoardNameColumnName = "boardName";
        public const string MaxTasksNumberColumnName = "maxTasks";
        public const string ColumnOrdinalColumName = "columnOrdinal";
        public const string ColumnNameColumnName = "columnName";

        private string _boardname;
        public string Boardname { get => _boardname; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _columnOrdinal, ColumnOrdinalColumName, BoardNameColumnName, value)) { _boardname = value; } } }


        private string _creator;
        public string Creator { get => _creator; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _columnOrdinal, ColumnOrdinalColumName, CreatorColumnName, value)) {_creator = value; } } }

        private int _columnOrdinal;
        public int ColumnOrdinal { get => _columnOrdinal; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _columnOrdinal, ColumnOrdinalColumName, ColumnOrdinalColumName, value)) { _columnOrdinal = value; } } }

        private string _columnName;
        public string ColumnName { get => _columnName; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _columnOrdinal, ColumnOrdinalColumName, ColumnNameColumnName, value)) { _columnName = value; } } }

        private int _maxTasksNumber;
        public int MaxTasksNumber { get => _maxTasksNumber; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, _columnOrdinal, ColumnOrdinalColumName, MaxTasksNumberColumnName, value)) _maxTasksNumber = value; } }

        





        private List<TaskDTO> _tasks;
        public List<TaskDTO> Tasks { get => _tasks; set { _tasks = value; } }




        public ColumnDTO(string creator, string boardname, int columnOrdinal, int maxTasks, string name, List<TaskDTO> tasks) : base(new ColumnDALController())
        {
            _columnName = name;
            _creator = creator;
            _boardname = boardname;
            _columnOrdinal = columnOrdinal;
            _maxTasksNumber = maxTasks;
            _tasks = tasks;

        }

        /// <summary>
        /// Insert new column to the DataBase
        /// </summary>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public override bool Insert()
        {
            return _controller.Insert(this);
        }

        /// <summary>
        /// Delete column and all it's tasks from the DataBase
        /// </summary>
        /// <returns>  This function return True if there are any records effected in the dataBase </returns>
        public override bool Delete()
        {
            bool res = _controller.Delete(this);
            if (res)
            {
                foreach (TaskDTO task in _tasks)
                {
                    res = res && task.Delete();
                }
            }
            return res;

        }

    }
}
