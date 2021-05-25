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
        /// <summary>
        /// This function insert new boardMember into board dataBase
        /// </summary>
        /// <param name="newMemeber"> The member who should be inserted into the board </param>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public bool InsertNewBoardMember(string newMemeber)
        {
            
            bool res = _controller.InsertNewBoardMember(this, newMemeber);
            if(res)
                _boardMembers.Add(newMemeber);
            return res;

        }

        /// <summary>
        /// Insert new board to the dataBase
        /// </summary>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public override bool Insert()
        {
            return _controller.Insert(this);
        }

        /// <summary>
        /// Delete board and all it's columns and tasks from the dataBase
        /// </summary>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
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
