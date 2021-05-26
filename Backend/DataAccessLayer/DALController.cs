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
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
        }
        /// <summary>
        /// /This function implmented in each controller and extract all the dto's of the specific controller from the dataBase
        /// </summary>
        /// <param name="query"></param>
        /// <returns> return list of DTO (implmented in the controllers) </returns>
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
                    CommandText = $"update {_tableName} set [{attributeName}]= '{attributeValue}' " +
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
                    $"where [{firstKeyColumnName}] = '{firstKey}' and [{secondKeyColumnName}] = '{secondKey}'" +
                    $" and [{thirdKeyColumnName}] = {thirdKey}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    command.Prepare();
                    Console.WriteLine(command.CommandText);
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
                    CommandText = $"update {_tableName} set [{attributeName}]={attributeValue} " +
                    $"where [{firstKeyColumnName}] = '{firstKey}' and [{secondKeyColumnName}] = '{secondKey}'" +
                    $" and [{thirdKeyColumnName}] = {thirdKey}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
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
                    CommandText = $"update {_tableName} set [{attributeName}]='{attributeValue}' where [{firstKeyColumnName}]='{firstKey}'"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
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
        /// <summary>
        /// this function is virtual for virtual inheritance via run time.
        /// this functionn implemented in BoardDALController.
        /// </summary>
        /// <param name="DTOobj"></param>
        /// <param name="newMember"></param>
        /// <returns></returns>
        public virtual bool InsertNewBoardMember(DTO DTOobj, string newMember)
        {
            return false;
        }


        public abstract bool Delete(DTO DTOobj);
        /// <summary>
        /// This function delete all recoreds from parm table's name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns> returns true if any recored effected </returns>
        public bool DeleteAllData(string tableName)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {tableName}"
                };
                try 
                { 
                    //command.Parameters.Add(new SQLiteParameter("@tableNameVal", tableName));

                    command.Prepare();
                    connection.Open();
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
    }
}


