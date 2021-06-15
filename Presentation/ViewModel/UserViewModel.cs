using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Presentation.ViewModel
{
    class UserViewModel : NotifiableObject
    {
        private BackendController controller;
        public UserModel User { get; private set; }
        private string _message;
        //public ObservableCollection<String> BoardsNames { get; set; }
        public string Message { get => _message; set { this._message = value;  } } //maybe no needed

        //should be string or BoardModel????
        private String _selectedMyBoard;
        public String SelectedMyBoard
        {
            get
            {
                return _selectedMyBoard;
            }
            set
            {
                _selectedMyBoard = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedMyBoard");
            }
        }


        private String _selectedAssignedBoard;
        public String SelectedAssignedBoard
        {
            get
            {
                return _selectedAssignedBoard;
            }
            set
            {
                _selectedAssignedBoard = value;
                EnableForward2 = value != null;
                RaisePropertyChanged("SelectedAssignedBoard");
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





        public UserViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.User = user;
            //this.BoardsNames = new ObservableCollection<String>();
            //if (controller.GetBoardNames(user.Email) != null)
            //{
            //    controller.GetBoardNames(user.Email).ToList().ForEach(BoardsNames.Add);
            //}
            //BoardsNames.CollectionChanged += HandleChange;
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

        public void RemoveBoard()
        {
            try
            {
                User.RemoveBoard(SelectedMyBoard);
                MessageBox.Show("Board Removed Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Remove this board. " + e.Message);

            }
        }

        public BoardModel SelectMyBoard()
        {
            return controller.GetMyBoard(User.Email, _selectedMyBoard);
        }

        public BoardModel SelectAssignedBoard()
        {
            return controller.GetAssignedBoard(User.Email, _selectedAssignedBoard);
        }




        //private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        RaisePropertyChanged("BoardsNames");
        //        CollectionViewSource.GetDefaultView(BoardsNames).Refresh();



        //    }
        //}







    }

    
}
