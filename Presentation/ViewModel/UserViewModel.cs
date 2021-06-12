using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class UserViewModel
    {
        private BackendController controller;
        private UserModel user;
        private string _message;
        public string Message { get => _message; set { this._message = value;  } } //maybe no needed

        public ObservableCollection<String> BoardsNames { get; set; }



        public UserViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.user = user;
            this.BoardsNames = user.BoardsNames;
        }

        internal void Logout()
        {
            Message = "";
            try
            {
                controller.Logout(user.Email);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return;
            }
        }





    }

    
}
