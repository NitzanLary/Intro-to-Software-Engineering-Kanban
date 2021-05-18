using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class UserDALController : DALController
    {
        private const string MessageTableName = "Users";

        public UserDALController() : base(MessageTableName)
        {

        }


        public List<UserDTO> SelectAllForums()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();

            return result;
        }



        public bool Insert(UserDTO user)
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

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", user.Id);
                    SQLiteParameter titleParam = new SQLiteParameter(@"nameVal", user.Name);

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
            UserDTO result = new UserDTO((long)reader.GetValue(0), reader.GetString(1));
            return result;

        }
    }
}
