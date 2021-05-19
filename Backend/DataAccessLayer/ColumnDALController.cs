using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class ColumnDALController : DALController
    {
        private const string ColumnsTableName = "Columns";


        private TaskDALController _taskDALController;


        public ColumnDALController() : base(ColumnsTableName)
        {
            _taskDALController = new TaskDALController();
        }

        internal List<ColumnDTO> SelectAllColumnsForBoard(string boardname, string creator)
        {
            string query = $"SELECT * FROM {ColumnsTableName} WHERE {ColumnDTO.CreatorColumnName} = {creator} and {ColumnDTO.BoardNameColumnName} = {boardname}";
            List<ColumnDTO> result = Select(query).Cast<ColumnDTO>().ToList();
            int columnOrdinal = 0;
            foreach (ColumnDTO c in result)
            {
                c.Tasks = _taskDALController.SelectAllTasksForColumn(boardname, creator, columnOrdinal++);
            }
            return result;
        }

        public bool InsertNewTask(TaskDTO board)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTO.foreignIDColumnName} ,{ForumDTO.ForumNameColumnName}) " +
                        $"VALUES (@idVal,@nameVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", board.Id);
                    SQLiteParameter titleParam = new SQLiteParameter(@"nameVal", board.Name);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        { 
            return new TaskDTO((long)reader.GetValue(0), reader.GetString(1));
        }
    }
}
