using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class BoardDALController : DALController
    {
        private const string MessageTableName = "Boards";

        public BoardDALController() : base(MessageTableName)
        {

        }


        public List<BoardDTO> SelectAllForums()
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();

            return result;
        }



        public bool Insert(BoardDTO board)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({DTO.IDColumnName} ,{ForumDTO.ForumNameColumnName}) " +
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
            BoardDTO result = new BoardDTO((long)reader.GetValue(0), reader.GetString(1));
            return result;

        }
    }
}

