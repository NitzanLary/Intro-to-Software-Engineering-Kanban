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
                if(value != null)
                    SelectedColumn = Board.Columns[value.ColumnOrdinal];
                //EnableForward = value != null;
                if (value != null && controller.IsCreator(user.Email, Board.Name))
                    EnableForward = true;
                EnableForward2 = value != null;

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

                if (value != null && controller.IsCreator(user.Email, Board.Name))
                    EnableForward = true;
                
                //EnableForward = value != null && controller.IsCreator(user.Email, Board.Name);
                RaisePropertyChanged("SelectedColumn");
            }
        }

        private string _filterText;
        public string FilterText { get => _filterText; set { this._filterText = value; RaisePropertyChanged("FilterText"); } }




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

        private bool _enableForward2 = false;
        public bool EnableForward2
        {
            get => _enableForward2;
            private set
            {
                _enableForward2 = value;
                RaisePropertyChanged("EnableForward2");
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
            if (SelectedColumn.ColumnOrdinal != Board.Columns.Count - 1)
            {
                Board.Columns[SelectedColumn.ColumnOrdinal + 1].Tasks.Add(SelectedTask);
                SelectedTask.ColumnOrdinal = SelectedTask.ColumnOrdinal + 1;
                SelectedColumn.Tasks.Remove(SelectedTask);
                MessageBox.Show("Task Advanced!");
            }
            else
            {
                MessageBox.Show("Cannot Advance Done Task");
            }





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

        internal void FilterTasks()
        {
            if (FilterText != null)
            {
                //Not Ideal, need to find a way to sort this list IN PLACE.
                ObservableCollection<TaskModel> NewColumnsTask = new ObservableCollection<TaskModel>();
                foreach (TaskModel t in SelectedColumn.Tasks)
                {

                    if (t.Title.Contains(FilterText) || t.Description.Contains(FilterText))
                    {
                        NewColumnsTask.Add(t);
                    }

                }
                SelectedColumn.Tasks = NewColumnsTask;
            }
        }



    }
}
