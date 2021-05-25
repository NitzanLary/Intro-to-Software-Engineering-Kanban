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
                    command.CommandText = $"DELETE FROM {BoardsTableName} WHERE [{BoardDTO.BoardNameColumnName}] = @boardNameVal " +
                $"and [{BoardDTO.CreatorColumnName}] = @boardCreatorVal";

                    SQLiteParameter creatorParam = new SQLiteParameter("@boardCreatorVal", board.Creator);
                    SQLiteParameter boardnameParam = new SQLiteParameter($"@boardNameVal", board.Boardname);



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

        public bool DeleteAllData()
        {
            bool restDeleted = _columnDALController.DeleteAllData();
            bool boardMemDeleted = DeleteAllData(BoardMembersTableName);
            bool boardsDeleted = DeleteAllData(BoardsTableName);
            return boardsDeleted || boardMemDeleted || restDeleted;
        }
    }
}

