using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{


    class AllInProgressTasksViewModel : NotifiableObject
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

        


    }
}
