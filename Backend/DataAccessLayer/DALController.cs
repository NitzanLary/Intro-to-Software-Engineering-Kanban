using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DALController
    {
        protected readonly string _connectionString;
        private readonly string _tableName;
        public DALController(string tableName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
        }

        protected List<DTO> Select(string query)
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = query;
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        protected List<DTO> Select()
        {
            string query = $"select * from {_tableName};";
            return Select(query);
        }


        protected abstract DTO ConvertReaderToObject(SQLiteDataReader reader);

        public bool Update(string firstKey, string firstKeyColumnName, string secondKey, string secondKeyColumnName, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeValue} " +
                    $"where {firstKeyColumnName} = '{firstKey}' and {secondKeyColumnName} = '{secondKey}'"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool Update(string firstKey, string firstKeyColumnName, string secondKey, string secondKeyColumnName
            , int thirdKey, string thirdKeyColumnName, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeValue} " +
                    $"where {firstKeyColumnName} = '{firstKey}' and {secondKeyColumnName} = '{secondKey}'" +
                    $" and {thirdKeyColumnName} = '{thirdKey}'"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool Update(string firstKey, string firstKeyColumnName, string secondKey, string secondKeyColumnName
            , int thirdKey, string thirdKeyColumnName, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeValue} " +
                    $"where {firstKeyColumnName} = '{firstKey}' and {secondKeyColumnName} = '{secondKey}'" +
                    $" and {thirdKeyColumnName} = '{thirdKey}'"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool Update(string firstKey, string firstKeyColumnName, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeName} where {firstKeyColumnName}='{firstKey}'"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
            }
            return res > 0;
        }


        public bool Delete(string query)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = query
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

    }
}


//    private SQLiteConnection GetConnection()
//        {
//            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

//            string connectionString = $"Data Source={path}; Version=3";
//            return new SQLiteConnection(connectionString);

//        }

//        const string messageTableNAme = "tablename";
//        const string IDColumnName = "ID";
//        - using
//        - SQLiteCommand command = new SQLiteCommand(null, connection);
//        int res = -1;
//        try{
//            command.CommandText = $"INSERT INTO {tablename} ({IDColumn},...)"+
//            $"VALUES (@idVal,...);"
//            SQLiteParameter idParam = new SQLiteParameter(@"idVal", 1);

//            SQLiteCommand.Paramenter.Add(idParam);
//            command.Prepare();

//            connection.Open();
//            res = SQLiteCommand.ExcecuteNonQuery(); (ששאילתת כתיבה)
//            }
//    catch (Exception e){
//        log error
//    }finally
//{
//    command.Dispose()
//    connecton.Close();
//}
//}
