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
        // can you see this?
        private string email;
        private bool isLoggedIn;

        public User(string email, bool isLoggedIn)
        {
            this.email = email;
            this.isLoggedIn = isLoggedIn;
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
    }
}
