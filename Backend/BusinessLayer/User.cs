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

        public Response<User> Login(string password)
        {
            if (!IsPasswordCorrect(password))
                return Response<User>.FromError("Incorrect Password");
            if (isLoggedIn)
                return Response<User>.FromError("User is already loogged in");
            IsLoggedIn = true;
            return Response<User>.FromValue(this);
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
            if (!isLoggedIn)
                return Response<User>.FromError("User is not loogged in");
            IsLoggedIn = false;
            return new Response();
        }

        private bool IsPasswordCorrect(string password)
        {
            return this.password.IsMatch(password);
        }
    }
}
