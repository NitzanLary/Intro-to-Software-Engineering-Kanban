using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class User
    {
        
        private string email;
        public string Email
        {
            get => email;
        }
        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set => isLoggedIn = value;
        }
        private Password password;


        public User(string email, Password password)
        {
            this.email = email;
            this.password = password;
        }

        public Response Login(string password)
        {
            if (!IsPasswordCorrect(password))
                return new Response("Incorrect Password");
            IsLoggedIn = true;
            return new Response();
        }
        
        public Response addBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        public Response removeBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        public Response logout()
        {
            throw new NotImplementedException();
        }

        private bool IsPasswordCorrect(string password)
        {
            return this.password.IsMatch(password);
        }
    }
}
