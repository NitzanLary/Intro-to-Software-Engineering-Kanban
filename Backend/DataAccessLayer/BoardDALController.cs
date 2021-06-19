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
    public class BoardDALController : DALController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private const string BoardsTableName = "Boards";
        private const string BoardMembersTableName = "BoardMembers";
        private const string MemberColumnName = "memberEmail";


        private ColumnDALController _columnDALController;

        public BoardDALController() : base(BoardsTableName)
        {
            _columnDALController = new ColumnDALController();
        }

        /// <summary>
        /// This function extract all the boards from the dataBase
        /// </summary>
        /// <returns> This fuction returns all the boards that in the dataBase within a list of type BoardDTO </returns>
        public List<BoardDTO> SelectAllBoards()
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();
            foreach (BoardDTO b in result)
            {
                b.Columns = _columnDALController.SelectAllColumnsForBoard(b.Boardname, b.Creator);
                b.BoardMembers = SelectAllBoardMembers(b.Boardname, b.Creator);
            }
            return result;
        }


        /// <summary>
        /// Insert new board to the dataBase, used by BoardDTO
        /// </summary>
        /// <param name="DTOobj"></param>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public override bool Insert(DTO DTOobj)
        {
            BoardDTO board = (BoardDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                Console.WriteLine(_connectionString);
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardsTableName} ({BoardDTO.CreatorColumnName} ,{BoardDTO.BoardNameColumnName}) " +
                        $"VALUES (@creatorVal,@boardnameVal);";

                    SQLiteParameter creatorParam = new SQLiteParameter(@"creatorVal", board.Creator);
                    SQLiteParameter boardnameParam = new SQLiteParameter(@"boardnameVal", board.Boardname);

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
        /// This function insert new boardMember into board dataBase used by boardDTO
        /// </summary>
        /// <param name="DTOobj"></param>
        /// <param name="newMemeber"></param>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public override bool InsertNewBoardMember(DTO DTOobj, string newMemeber)
        {
            BoardDTO board = (BoardDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardMembersTableName} ({MemberColumnName}, {BoardDTO.BoardNameColumnName} ,{BoardDTO.CreatorColumnName}) " +
                        $"VALUES (@boardMemberVal,@boardnameVal,@creatorVal);";

                    SQLiteParameter creatorParam = new SQLiteParameter(@"creatorVal", board.Creator);
                    SQLiteParameter boardnameParam = new SQLiteParameter(@"boardnameVal", board.Boardname);
                    SQLiteParameter boardMemberParam = new SQLiteParameter(@"boardMemberVal", newMemeber);

                    command.Parameters.Add(creatorParam);
                    command.Parameters.Add(boardnameParam);
                    command.Parameters.Add(boardMemberParam);
                    command.Prepare();

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
                return res > 0;
            }
        }
        /// <summary>
        /// This function converd dataBase reader object(what return's from the dataBase) into boardDTO DTO.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new BoardDTO(reader.GetString(0), reader.GetString(1), null, null);
        }


        protected List<string> SelectAllBoardMembers(string boardName, string creator)
        {
            List<string> results = new List<string>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT {MemberColumnName} FROM {BoardMembersTableName} WHERE {BoardDTO.CreatorColumnName} = '{creator}' and {BoardDTO.BoardNameColumnName} = '{boardName}'";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(dataReader.GetString(0));

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


        public override bool Delete(DTO DTOobj)
        {

            BoardDTO board = (BoardDTO)DTOobj;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM {BoardsTableName} WHERE [{BoardDTO.BoardNameColumnName}] = @boardNameVal1 " +
                $"and [{BoardDTO.CreatorColumnName}] = @boardCreatorVal1;" +
                $"DELETE FROM {BoardMembersTableName} WHERE [{BoardDTO.BoardNameColumnName}] = @boardNameVal2 " +
                $"and [{BoardDTO.CreatorColumnName}] = @boardCreatorVal2;";

                    SQLiteParameter creatorParam1 = new SQLiteParameter("@boardCreatorVal1", board.Creator);
                    SQLiteParameter boardnameParam1 = new SQLiteParameter($"@boardNameVal1", board.Boardname);
                    SQLiteParameter creatorParam2 = new SQLiteParameter("@boardCreatorVal2", board.Creator);
                    SQLiteParameter boardnameParam2 = new SQLiteParameter($"@boardNameVal2", board.Boardname);



                    command.Parameters.Add(creatorParam1);
                    command.Parameters.Add(boardnameParam1);
                    command.Parameters.Add(creatorParam2);
                    command.Parameters.Add(boardnameParam2);
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
        /// This function delete all board and its related records from the dataBase.
        /// </summary>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public bool DeleteAllData()
        {
            bool restDeleted = _columnDALController.DeleteAllData();
            bool boardMemDeleted = DeleteAllData(BoardMembersTableName);
            bool boardsDeleted = DeleteAllData(BoardsTableName);
            return boardsDeleted || boardMemDeleted || restDeleted;
        }
    }
}

