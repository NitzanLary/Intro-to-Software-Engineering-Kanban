using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class AddBoardViewModel : NotifiableObject
    {
        private BackendController controller;
        private UserModel user;
        private string _boardName;
        public string BoardName { get => _boardName; set { this._boardName = value; RaisePropertyChanged("BoardName"); } }

        public AddBoardViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.user = user;
            
        }

        public void AddBoard()
        {
            try
            {
                user.AddBoard(BoardName);
                MessageBox.Show("Board Added Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot add new board. " + e.Message);
                
            }
        }



    }
}
