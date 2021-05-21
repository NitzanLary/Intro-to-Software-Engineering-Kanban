using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DALController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                catch(Exception e)
                {
                    log.Error(e.Message);
                    throw;
                    
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
                    $"where {firstKeyColumnName} = @{firstKey} and {secondKeyColumnName} = @{secondKey}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeValue, attributeValue));
                    command.Parameters.Add(new SQLiteParameter(firstKey, firstKey));
                    command.Parameters.Add(new SQLiteParameter(secondKey, secondKey));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    throw;
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
                    CommandText = $"update {_tableName} set [{attributeName}]='{attributeValue}' " +
                    $"where [{firstKeyColumnName}] = @{firstKey} and [{secondKeyColumnName}] = @{secondKey}" +
                    $" and [{thirdKeyColumnName}] = @{thirdKey}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeValue, attributeValue));
                    command.Parameters.Add(new SQLiteParameter($"@{firstKey}", firstKey));
                    command.Parameters.Add(new SQLiteParameter($"@{secondKey}", secondKey));
                    command.Parameters.Add(new SQLiteParameter($"@{thirdKey}", thirdKey));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    throw;
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
                    $"where [{firstKeyColumnName}] = @{firstKey} and [{secondKeyColumnName}] = @{secondKey}" +
                    $" and [{thirdKeyColumnName}] = @{thirdKey}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter($"@{attributeValue}", attributeValue));
                    command.Parameters.Add(new SQLiteParameter($"@{firstKey}", firstKey));
                    command.Parameters.Add(new SQLiteParameter($"@{secondKey}", secondKey));
                    command.Parameters.Add(new SQLiteParameter($"@{thirdKey}", thirdKey));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    throw;
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
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeValue} where [{firstKeyColumnName}]=@{firstKey}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter($"@{attributeValue}", attributeValue));
                    command.Parameters.Add(new SQLiteParameter($"@{firstKey}", firstKey));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
            }
            return res > 0;
        }

        public abstract bool Insert(DTO DTOobj); 


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
                catch(Exception e)
                {
                    log.Error(e.Message);
                    throw;
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


