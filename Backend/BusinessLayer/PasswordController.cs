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

        public PasswordController(int min_len, int max_len)
        {
            this.MIN_LEN = min_len;
            this.MAX_LEN = max_len;
        }

        public Response<bool> isValid(string password)
        {
            return null;
        }

        public Response<Password> createPassword(string password)
        {
            return null;
        } 
    }
}
