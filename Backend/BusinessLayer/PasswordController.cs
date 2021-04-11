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
        private static PasswordController passwordController = null;
        //private Response<bool> Response;

        public PasswordController()
        {
            this.MIN_LEN = 4;
            this.MAX_LEN = 20;
        }

        public static PasswordController GetInstance()
        {
            if (passwordController == null)
                passwordController = new PasswordController();
            return passwordController;
        }

        public Response<bool> isValid(string password)
        {
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            if (password.Length < MIN_LEN)
            {
                return Response<bool>.FromError("The password is too short, you need at least " + MIN_LEN + " charcters");
            }
            
            if (password.Length > MAX_LEN)
            {
                return Response<bool>.FromError("The password is too long, you need at most " + MAX_LEN + " charcters");
            }
            foreach (char c in password){
                if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(c))
                    flag1 = true;
                if ("abcdefghijklmnopqrstuvwxyz".Contains(c))
                    flag1 = true;
                if ("1234567890".Contains(c))
                    flag1 = true;
            }
            if (flag1 && flag3 && flag2)
                return Response<bool>.FromValue(true);
            return Response<bool>.FromError("Password must include atleast one uppercase letter, one small character and a number.");
            
        }

        public Response<Password> createPassword(string password)
        {
            Response<bool> r = isValid(password);
            if (r.ErrorOccured)
                return Response<Password>.FromError(r.ErrorMessage);
            Password pass = new Password(password);
            return Response<Password>.FromValue(pass);
        } 
    }
}
