using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Password
    {
        private string password;
        public string Password_
        {
            get => password;
            private set => password = value;
        }

        public Password(string pass)
        {
            this.password = pass;
        }

        /// <summary>
        /// Checks if a given string type password is the same as the password field.
        /// </summary>
        /// <param name="other">The password we want to check if is match</param>
        ///  <returns>True if they are match else false</returns>
        public bool IsMatch(string other) { return password == other; }
    }
}
    