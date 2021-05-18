using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class BoardDALController : DALController
    {
        private const string BoardsTableName = "Boards";
        private const string TasksTableName = "Tasks";
        private const string BoardMembersTableName = "BoardMemebers";

        public BoardDALController() : base(BoardsTableName)
        {

        }


        public List<BoardDTO> SelectAllBoards()
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();
            foreach (BoardDTO b in result)
            {
                for (int i = 0; i < b.Columns.Count; i++)
                {
                    string getAllTasksQuery = $"select * from {TasksTableName} " +
                        $"where {DTO.BoardNameColumnName} = {b.Boardname} and {DTO.CreatorColumnName} = {b.Creator} " +
                        $"and {TaskDTO.ColumnOrdinalColumnName} = {i};";
                    b.Columns[0].Tasks = Select(getAllTasksQuery).Cast<TaskDTO>().ToList();
                    b.Columns[0]
                }
            }
            return result;
        }



        public bool InsertNewBoard(BoardDTO board)
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

        public bool InsertNewBoardMember(string newMemeber)
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
            if (reader.GetSchemaTable().TableName.Equals(TasksTableName))
                return new TaskDTO();
            return new BoardDTO((long)reader.GetValue(0), reader.GetString(1));

        }
    }
}

