using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class PasswordController
    {
        private int MIN_LEN;
        private int MAX_LEN;
        //private Response<bool> Response;

        public PasswordController()
        {
            this.MIN_LEN = 4;
            this.MAX_LEN = 20;
        }

        public Response<bool> isValid(string password)
        {
            if (password.Length < MIN_LEN)
            {
                return Response<bool>.FromError("The password is too short, you need at least " + MIN_LEN + " charcters");
            }
            
            if (password.Length > MAX_LEN)
            {
                return Response<bool>.FromError("The password is too long, you need at most " + MAX_LEN + " charcters");
            }
            foreach (char c in password){

            }
            return null;

        }

        public Response<Password> createPassword(string password)
        {
            return null;
        } 
    }
}
