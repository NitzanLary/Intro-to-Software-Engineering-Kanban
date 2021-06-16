using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{


    public class AllInProgressTasksViewModel : NotifiableObject
    {
        private BackendController controller;

        //public List<TaskModel> Tasks { get; set; }

        public UserModel User { get; private set; }
        private string _message;
        public string Message { get => _message; set { this._message = value; } } //maybe no needed

        //should be string or TaskModel????
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

        private string _taskDueDate;
        public string TaskDueDate { get => _taskDueDate; set { this._taskDueDate = value; RaisePropertyChanged("TaskDueDate"); } }


        private string _filterText;
        public string FilterText { get => _filterText; set { this._filterText = value; RaisePropertyChanged("FilterText"); } }


        public AllInProgressTasksViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.User = user;
            
            
        }

        internal void Logout()
        {
            Message = "";
            try
            {
                controller.Logout(User.Email);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return;
            }
        }

        internal void SortTasksByDueDate()
        {
            //Not Ideal, need to find a way to sort this list IN PLACE.
             User.InProgressTasks = new ObservableCollection<TaskModel>(User.InProgressTasks.OrderBy(d => d.DueDate).ToList());
        }

        internal void FilterTasks()
        {
            //Not Ideal, need to find a way to sort this list IN PLACE.
            ObservableCollection<TaskModel>  NewInProgressTasks = new ObservableCollection<TaskModel>();
            foreach(TaskModel t in User.InProgressTasks)
            {
                if(t.Title.Contains(FilterText) || t.Description.Contains(FilterText))
                {
                    NewInProgressTasks.Add(t);
                }
                User.InProgressTasks = NewInProgressTasks;
            }
        }

        public void EditTaskDueDate()
        {
            try
            {
                controller.UpdateTaskDueDate(User.Email, SelectedTask.Creator, SelectedTask.BoardName, SelectedTask.ColumnOrdinal, SelectedTask.TaskID, DateTime.Parse(TaskDueDate));

                MessageBox.Show("Task DueDate Edited Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Edit Task DueDate. " + e.Message);

            }
        }








    }
}
