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

        public Password(string pass)
        {
            this.password = pass;
        }

        public bool IsMatch(string other) { return password == other; }
    }
}
    