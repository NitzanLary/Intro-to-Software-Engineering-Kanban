using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class BoardViewModel : NotifiableObject
    {
        //should be string or TaskModel????

        private BackendController controller;


        private TaskModel _selectedTask;
        public TaskModel SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedTask");
            }
        }

        private ColumnModel _selectedColumn;
        public ColumnModel SelectedColumn
        {
            get
            {
                return _selectedColumn;
            }
            set
            {
                _selectedColumn = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedColumn");
            }
        }

        

        private bool _enableForward = false;
        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }
        public BoardModel Board { get; set; }

        UserModel user;



        public BoardViewModel(UserModel user, BoardModel board)
        {
            this.user = user;
            this.Board = board;
            this.controller = user.Controller;
        }

        public void SelectedItem(TaskModel task)
        {
            TaskModel SelectedTask = task;
            RaisePropertyChanged("SelectedTask");
        }

        public void RemoveColumn()
        {
            try
            {
                controller.RemoveColumn(user.Email, Board.Creator, Board.Name, SelectedColumn.ColumnOrdinal);
                MessageBox.Show("Board Removed Column!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Remove this Column. " + e.Message);

            }
        }

        public void AdvanceTask()
        {
            try
            {
                controller.AdvanceTask(user.Email, Board.Creator, Board.Name, SelectedColumn.ColumnOrdinal, SelectedTask.TaskID);
                MessageBox.Show("Board Removed Column!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Remove this Column. " + e.Message);

            }
        }

        public void MoveColumnLeft()
        {
            try
            {
                controller.MoveColumn(user.Email, Board.Creator, Board.Name, SelectedColumn.ColumnOrdinal, -1);
                MessageBox.Show("Column moved left!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot move left this column. " + e.Message);

            }

        }

        public void MoveColumnRight()
        {
            try
            {
                controller.MoveColumn(user.Email, Board.Creator, Board.Name, SelectedColumn.ColumnOrdinal, 1);
                MessageBox.Show("Column moved right!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot move right this column. " + e.Message);

            }

        }
    }
}
