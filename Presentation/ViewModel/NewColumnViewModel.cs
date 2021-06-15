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

        private string _columnOrdinal;
        public string ColumnOrdinal { get => _columnOrdinal; set { this._columnOrdinal = value; RaisePropertyChanged("ColumnOrdinal"); } }

        private string _columnName;
        public string ColumnName { get => _columnName; set { this._columnName = value; RaisePropertyChanged("ColumnName"); } }

        private string _maxTasks;
        public string MaxTasks { get => _maxTasks; set { this._maxTasks = value; RaisePropertyChanged("MaxTasks"); } }



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
                board.AddColumn(user.Email, board.Creator, board.Name, int.Parse(ColumnOrdinal), ColumnName, int.Parse(MaxTasks));
                //should it be defult maxtasks to unlimit?....
                //controller.LimitColumn(user.Email, board.Creator, board.Name, int.Parse(ColumnOrdinal), int.Parse(MaxTasks));
                MessageBox.Show("Column Added Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot add new Column. " + e.Message);

            }
        }


    }
}
