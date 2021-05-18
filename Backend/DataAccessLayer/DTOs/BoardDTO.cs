using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO : DTO
    {
        public const string CreatorColumnName = "Creator";
        public const string BoardNameColumnName = "BoardName";
        public const string MaxBacklogsColumnName = "MBL";
        public const string MaxInProgressColumnName = "MIP";
        public const string MaxInDoneColumnName = "MID";

        private string _boardname;
        public string Boardname { get => _boardname; set { _boardname = value; _controller.update(_boardname, _creator, BoardNameColumnName, value ); } }


        private string _creator;
        public string Creator { get => _creator; set { _creator = value; _creator.update(_boardname, _creator, CreatorColumnName, value); } }


        private int _maxBackLog;
        public int MaxBackLog { get => _maxBackLog; set { _maxBackLog = value; _maxBackLog.update(_boardname, _creator, MaxBacklogsColumnName, value); } }


        private int _maxInProgress;
        public int MaxInProgress { get => _maxInProgress; set { _maxInProgress = value; _maxInProgress.update(_boardname, _creator, MaxInProgressColumnName, value); } }


        private int _maxInDone;
        public int MaxInDone { get => _maxInDone; set { _maxInDone = value; _maxInDone.update(_boardname, _creator, MaxInDoneColumnName, value); } }


        private List<string> _boardMembers;
        public List<string> BoardMembers { get => _boardMembers; }

        private List<ColumnDTO> _columns;
        public List<ColumnDTO> Columns { get => _columns; }




        public BoardDTO(string creator, string boardname, int maxBackLog, int maxInProgress, int maxInDone, List<string> boardMembers, List<ColumnDTO> columns) : base(new BoardDALController())
        {
            _boardname = boardname;
            _creator = creator;
            _maxBackLog = maxBackLog;
            _maxInProgress = maxInProgress;
            _maxInDone = maxInDone;
            _boardMembers = boardMembers;
            _columns = columns;
        }

    }
}
