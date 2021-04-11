using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class UserController
    {
        private readonly PasswordController pc;
        public Response Register(string email, string password)
        {
            throw new NotImplementedException();
            
        }

        public Response Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Response Logout(string email)
        {
            throw new NotImplementedException();
        }

        public Response<User> getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Response<Boolean> isLoggedIn(string email)
        {
            throw new NotImplementedException();
        }

    }
}