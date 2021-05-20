﻿using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDALController : DALController
    {
        private const string UsersTableName = "Users";

        public UserDALController() : base(UsersTableName)
        {

        }


        public List<UserDTO> SelectAllForums()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();

            return result;
        }



        public bool InsertNewUser(UserDTO user)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {UsersTableName} ({UserDTO.EmailColumnName} ,{UserDTO.PasswordColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"passwordVal", user.Password);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //log error
                    Console.WriteLine(e.Message);
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
            return new UserDTO(reader.GetString(0), reader.GetString(1));
        }
    }
}