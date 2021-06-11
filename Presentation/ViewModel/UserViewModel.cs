using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class UserViewModel
    {
        private BackendController _controller;
        private UserModel _user;
        private string _message;
        public string Message { get => _message; set { this._message = value;  } } //maybe no needed

        public List<BoardModel> MyBoards { get; private set; }




        public UserViewModel(UserModel user)
        {
            this._controller = user.Controller;
            this._user = user;
        }

        internal void Logout()
        {
            Message = "";
            try
            {
                _controller.Logout(_user.Email);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return;
            }
        }





    }

    
}
