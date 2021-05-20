using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    public class BoardDTO : DTO
    {
        public const string CreatorColumnName = "boardCreator";
        public const string BoardNameColumnName = "boardName";


        private string _boardname;
        public string Boardname { get => _boardname; set { _controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, BoardNameColumnName, value ); _boardname = value; } }


        private string _creator;
        public string Creator { get => _creator; set { _controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, CreatorColumnName, value); _creator = value; } }



        private List<string> _boardMembers;
        public List<string> BoardMembers { get => _boardMembers; set { _boardMembers = value; } }

        private List<ColumnDTO> _columns;
        public List<ColumnDTO> Columns { get => _columns; set { _columns = value; } }




        public BoardDTO(string creator, string boardname, List<string> boardMembers, List<ColumnDTO> columns) : base(new BoardDALController())
        {
            _boardname = boardname;
            _creator = creator;
            _boardMembers = boardMembers;
            _columns = columns;
        }

        public int AddBoardMemeber(string newMemeber)
        {
            throw new NotImplementedException();
        }

    }
}
