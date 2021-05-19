using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDALController : DALController
    {
        private const string TasksTableName = "Tasks";

        public TaskDALController() : base(TasksTableName)
        {

        }


        public List<TaskDTO> SelectAllTasksForColumn(string boardName, string creator, long columnOrdinal)
        {
            string query = $"SELECT * FROM {TasksTableName} " +
                $"WHERE {TaskDTO.CreatorColumnName} = {creator} and {TaskDTO.BoardNameColumnName} = {boardName} and {TaskDTO.ColumnOrdinalColumnName} = {columnOrdinal} ";
            List<TaskDTO> result = Select().Cast<TaskDTO>().ToList();
            return result;
        }



        public bool InsertNewTask(TaskDTO task)
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
            //return new TaskDTO((long)reader.GetValue(0), reader.GetString(1));
            throw new NotImplementedException();
        }
    }
}
