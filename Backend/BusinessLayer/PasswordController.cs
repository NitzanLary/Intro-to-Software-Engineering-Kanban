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

        public PasswordController()
        {
            this.MIN_LEN = 4;
            this.MAX_LEN = 20;
        }

        /// <summary>
        /// Check validity of a given password
        /// </summary>
        /// <param name="password">The password we want to check if is valid</param>
        ///  <returns>A response<bool> object. The response should contain an error message in case of missing board for user or invalid argments</returns>
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
                    flag2 = true;
                if ("1234567890".Contains(c))
                    flag3 = true;
            }
            if (flag1 && flag3 && flag2)
                return Response<bool>.FromValue(true);
            return Response<bool>.FromError("Password must include atleast one uppercase letter, one small character and a number.");
            
        }

        /// <summary>
        /// Create a Password object from a given string type password 
        /// </summary>
        /// <param name="password">The password we want to create the object from</param>
        /// <returns>A Response<Password> object</returns>
        public Response<Password> createPassword(string password)
        {
            Response<bool> r = isValid(password);
            if (r.ErrorOccured)
                return Response<Password>.FromError(r.ErrorMessage);
            if (password == null)
                return Response<Password>.FromError("password can not be null");
            Password pass = new Password(password);
            return Response<Password>.FromValue(pass);
        } 
    }
}
