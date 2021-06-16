using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class EditColumnViewModel : NotifiableObject
    {
        private BackendController controller;
        private UserModel user;
        private BoardModel board;
        private ColumnModel column;

        //private string _columnOrdinal;
        //public string ColumnOrdinal { get => _columnOrdinal; set { this._columnOrdinal = value; RaisePropertyChanged("ColumnOrdinal"); } }

        private string _columnName;
        public string ColumnName { get => _columnName; set { this._columnName = value; RaisePropertyChanged("ColumnName"); } }

        private string _maxTasks;
        public string MaxTasks { get => _maxTasks; set { this._maxTasks = value; RaisePropertyChanged("MaxTasks"); } }



        public EditColumnViewModel(UserModel user, BoardModel board, ColumnModel column)
        {
            this.controller = user.Controller;
            this.user = user;
            this.board = board;
            this.column = column;

            //this._columnOrdinal = ColumnOrdinal;
            this._maxTasks = MaxTasks;
            this._columnName = ColumnName;
        }

        public void EditColumn()
        {
            try
            {
                controller.RenameColumn(user.Email, board.Creator, board.Name, column.ColumnOrdinal, ColumnName);
                column.Name = ColumnName;
                controller.LimitColumn(user.Email, board.Creator, board.Name, column.ColumnOrdinal, int.Parse(MaxTasks));
                column.MaxTasks = int.Parse(MaxTasks);
                

                //What is Shift SIZE????
                MessageBox.Show("Column Edited Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Edit Column. " + e.Message);

            }
        }
    }
}
