using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private String _selectedBoard;
        public String SelectedBoard
        {
            get
            {
                return _selectedBoard;
            }
            set
            {
                _selectedBoard = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedBoard");
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
