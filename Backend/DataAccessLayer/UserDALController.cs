using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDALController : DALController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private const string UsersTableName = "Users";

        public UserDALController() : base(UsersTableName)
        {

        }

        /// <summary>
        /// extract all users from dataBase
        /// </summary>
        /// <returns> return a list of UserDTO which extracted from the dataBase </returns>
        public List<UserDTO> SelectAllUsers()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();

            return result;
        }

        /// <summary>
        /// convert the reader(sql return type) into the correct DTO via the dto's constructor
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new UserDTO(reader.GetString(0), reader.GetString(1));
        }
        /// <summary>
        /// Insert new User into the dataBase
        /// </summary>
        /// <param name="DTOobj"></param>
        /// <returns> return true if any recored affected </returns>
        public override bool Insert(DTO DTOobj)
        {
            UserDTO user = (UserDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                Console.WriteLine(_connectionString);
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {UsersTableName} ({UserDTO.EmailColumnName} ,{UserDTO.PasswordColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal);";

                    SQLiteParameter creatorParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter boardnameParam = new SQLiteParameter(@"passwordVal", user.Password);

                    command.Parameters.Add(creatorParam);
                    command.Parameters.Add(boardnameParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //log error
                    log.Error(e.Message);
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        /// <summary>
        /// delete user from the dataBase
        /// </summary>
        /// <param name="DTOobj"></param>
        /// <returns> return true if any recored affected </returns>
        public override bool Delete(DTO DTOobj)
        {

            UserDTO user = (UserDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                Console.WriteLine(_connectionString);
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM {UsersTableName} WHERE [{UserDTO.EmailColumnName}] = @emailVal " ;

                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", user.Email);



                    command.Parameters.Add(emailParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //log error
                    log.Error(e.Message);
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }

        }

        /// <summary>
        /// Delete all recoreds from table Users
        /// </summary>
        /// <returns> return true if any recored affected </returns>
        public bool DeleteAllData()
        {
            return DeleteAllData(UsersTableName);
        }

    }
}
