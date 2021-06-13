using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class BoardViewModel
    {
        private BoardModel _selectedBoard;
        public BoardModel Board { get; set; }

        UserModel user;


        public BoardViewModel(UserModel user, BoardModel board)
        {
            this.user = user;
            this.Board = board;
        }
    }
}
