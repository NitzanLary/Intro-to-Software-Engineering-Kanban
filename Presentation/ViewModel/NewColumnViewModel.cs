using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class NewColumnViewModel : NotifiableObject
    {
        private BackendController controller;
        private UserModel user;
        private BoardModel board;

        private int _columnOrdinal;
        public int ColumnOrdinal { get => _columnOrdinal; set { this._columnOrdinal = value; RaisePropertyChanged("ColumnOrdinal"); } }

        private string _columnName;
        public string ColumnName { get => _columnName; set { this._columnName = value; RaisePropertyChanged("ColumnName"); } }



        public NewColumnViewModel(UserModel user, BoardModel board)
        {
            this.controller = user.Controller;
            this.user = user;
            this.board = board;
        }

        public void AddColumn()
        {
            try
            {
                controller.AddColumn(user.Email, board.Creator, board.Name, ColumnOrdinal, ColumnName);
                MessageBox.Show("Column Added Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot add new Column. " + e.Message);

            }
        }


    }
}
