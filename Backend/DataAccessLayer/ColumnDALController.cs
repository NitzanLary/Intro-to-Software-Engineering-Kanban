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
    public class ColumnDALController : DALController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string ColumnsTableName = "Columns";
        private TaskDALController _taskDALController;


        public ColumnDALController() : base(ColumnsTableName)
        {
            _taskDALController = new TaskDALController();
        }
        /// <summary>
        /// this function extract all columns for board "boardName" from the dataBase
        /// </summary>
        /// <param name="boardname"></param>
        /// <param name="creator"></param>
        /// <returns> This functions return list of clumonsDTO </returns>
        internal List<ColumnDTO> SelectAllColumnsForBoard(string boardname, string creator)
        {
            string query = $"SELECT * FROM {ColumnsTableName} " +
                $"WHERE {ColumnDTO.CreatorColumnName} = '{creator}' and {ColumnDTO.BoardNameColumnName} =  '{boardname}'" +
                $" ORDER BY {ColumnDTO.ColumnOrdinalColumName}";
            List<ColumnDTO> result = Select(query).Cast<ColumnDTO>().ToList();
            int columnOrdinal = 0;
            foreach (ColumnDTO c in result)
            {
                c.Tasks = _taskDALController.SelectAllTasksForColumn(boardname, creator, columnOrdinal++);
            }
            return result;
        }

        public override bool Insert(DTO DTOobj)
        {
            ColumnDTO column = (ColumnDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {ColumnsTableName} ({ColumnDTO.CreatorColumnName}" +
                        $"               ,{ColumnDTO.BoardNameColumnName}, {ColumnDTO.ColumnOrdinalColumName}" +
                        $"               , {ColumnDTO.MaxTasksNumberColumnName}, {ColumnDTO.ColumnNameColumnName}) " +
                        $"VALUES (@boardCreatorVal, @boardNameVal, @columnOrdinalVal, @maxTasksVal, @columnNameVal);";

                    SQLiteParameter creatorParam = new SQLiteParameter(@"boardCreatorVal", column.Creator);
                    SQLiteParameter boardNameParam = new SQLiteParameter(@"boardNameVal", column.Boardname);
                    SQLiteParameter columnOrdParam = new SQLiteParameter(@"columnOrdinalVal", column.ColumnOrdinal);
                    SQLiteParameter maxTasksParam = new SQLiteParameter(@"maxTasksVal", column.MaxTasksNumber);
                    SQLiteParameter columnNameParam = new SQLiteParameter(@"columnNameVal", column.ColumnName);


                    command.Parameters.Add(creatorParam);
                    command.Parameters.Add(boardNameParam);
                    command.Parameters.Add(columnOrdParam);
                    command.Parameters.Add(maxTasksParam);
                    command.Parameters.Add(columnNameParam);


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

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new ColumnDTO(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), null);
        }

        public override bool Delete(DTO DTOobj)
        {

            ColumnDTO column = (ColumnDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                Console.WriteLine(_connectionString);
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM {ColumnsTableName} WHERE [{ColumnDTO.BoardNameColumnName}] = @boardNameVal " +
                $"and [{ColumnDTO.CreatorColumnName}] = @boardCreatorVal and [{ColumnDTO.ColumnOrdinalColumName}] = @columnOrdinalVal" ;

                    SQLiteParameter creatorParam = new SQLiteParameter("@boardCreatorVal", column.Creator);
                    SQLiteParameter boardnameParam = new SQLiteParameter($"@boardNameVal", column.Boardname);
                    SQLiteParameter columnordinalParam = new SQLiteParameter($"@columnOrdinalVal", column.ColumnOrdinal);




                    command.Parameters.Add(creatorParam);
                    command.Parameters.Add(boardnameParam);
                    command.Parameters.Add(columnordinalParam);
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
        /// this function delete all the columns from the dataBase
        /// </summary>
        /// <returns> return true if any records affected </returns>
        public bool DeleteAllData()
        {
            bool tasksDeleted = _taskDALController.DeleteAllData();
            bool columnDeleted = DeleteAllData(ColumnsTableName);
            return columnDeleted && tasksDeleted;
        }

    }
}
