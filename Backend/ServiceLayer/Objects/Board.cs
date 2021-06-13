using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    public struct Board
    {
        public readonly string name;
        public readonly string creator;
        public List<Column> columns;
        internal Board(string name, string creator, List<Column> columns)
        {
            this.name = name;
            this.creator = creator;
            this.columns = columns;
        }

        internal Board(BusinessLayer.Board board)
        {
            this.columns = board.Columns.Select(c => new Column(c)).ToList();
            this.name = board.Name;
            this.creator = board.Creator;
            
        }
    }
}
