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
        public string Boardname { get => _boardname; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, BoardNameColumnName, value)) { _boardname = value; } } }


        private string _creator;
        public string Creator { get => _creator; set { if (_controller.Update(_boardname, BoardNameColumnName, _creator, CreatorColumnName, CreatorColumnName, value)) { _creator = value; } } }



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

        // Add member to the '_boardMembers' + add to the right table at the DB.
        // Untill 22/05/2021 23:59
        public bool InsertNewBoardMember(string newMemeber)
        {
            
            bool res = _controller.InsertNewBoardMember(this, newMemeber);
            if(res)
                _boardMembers.Add(newMemeber);
            return res;

        }

        public override bool Insert()
        {
            return _controller.Insert(this);
        }

        public override bool Delete()
        {
            bool res = _controller.Delete(this);
            if (res)
            {
                foreach (ColumnDTO column in _columns)
                {
                    res = res && column.Delete();
                }
            }
            return res;

        }



    }
}
