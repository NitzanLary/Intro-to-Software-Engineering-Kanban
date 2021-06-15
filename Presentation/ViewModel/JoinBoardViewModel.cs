using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class JoinBoardViewModel : NotifiableObject
    {
        private BackendController controller;
        private UserModel user;
        private string _boardName;
        public string BoardName { get => _boardName; set { this._boardName = value; RaisePropertyChanged("BoardName"); } }

        private string _boardCreator;
        public string BoardCreator { get => _boardCreator; set { this._boardCreator = value; RaisePropertyChanged("BoardCreator"); } }

        public JoinBoardViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.user = user;

        }

        public void joinBoard()
        {
            try
            {
                controller.JoinBoard(user.Email, BoardCreator, BoardName);
                MessageBox.Show("Board joined successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot join this board. " + e.Message);

            }
        }
    }
}
