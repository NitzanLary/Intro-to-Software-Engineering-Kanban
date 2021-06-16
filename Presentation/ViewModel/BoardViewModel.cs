using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
                //SelectedColumn = Board.Columns[_selectedTask.ColumnOrdinal];
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
                EnableForward = value != null/* && controller.IsCreator(user.Email, Board.Name)*/;
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

        //private bool _enableForward2 = false;
        //public bool EnableForward2
        //{
        //    get => _enableForward2;
        //    private set
        //    {
        //        _enableForward2 = value;
        //        RaisePropertyChanged("EnableForward2");
        //    }
        //}
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
                Board.RemoveColumn(SelectedColumn);
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
                //controller.AdvanceTask(user.Email, Board.Creator, Board.Name, SelectedColumn.ColumnOrdinal, SelectedTask.TaskID);
                if (SelectedTask.ColumnOrdinal == 0)
                    user.InProgressTasks.Add(SelectedTask);
                //if (SelectedTask.ColumnOrdinal == Board.Columns.Count - 1)
                //    user.InProgressTasks.Remove(SelectedTask);
                Board.Columns[SelectedColumn.ColumnOrdinal + 1].Tasks.Add(SelectedTask);
                SelectedTask.ColumnOrdinal = SelectedTask.ColumnOrdinal + 1;
                SelectedColumn.Tasks.Remove(SelectedTask);
                
                

                MessageBox.Show("Task Advanced!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Advance This Task. " + e.Message);

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

        internal void Logout()
        {
            try
            {
                controller.Logout(user.Email);
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Logout. " + e.Message);
            }
        }

        internal void SortTasksByDueDate()
        {
            //Not Ideal, need to find a way to sort this list IN PLACE.
            SelectedColumn.Tasks = new ObservableCollection<TaskModel>(SelectedColumn.Tasks.OrderBy(d => d.DueDate).ToList());
        }

        


    }
}
