using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class BoardViewModel : NotifiableObject
    {
        //should be string or BoardModel????

        private String _selectedTask;
        public String SelectedTask
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

        private String _selectedColumn;
        public String SelectedColumn
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
        }

        public void SelectedItem(TaskModel task)
        {
            TaskModel SelectedTask = task;
            RaisePropertyChanged("SelectedTask");
        }
    }
}
