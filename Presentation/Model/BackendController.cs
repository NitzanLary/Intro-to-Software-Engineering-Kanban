using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class BackendController
    {
        private Service Service { get; set; }

        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }

        public BackendController(Service service)
        {
            this.Service = service;
        }

        public UserModel Login(string username, string password)
        {
            Response<User> user = Service.Login(username, password);
            if (user.ErrorOccured)
            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(this, username);
        }

        internal void Register(string username, string password)
        {
            Response res = Service.Register(username, password);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void Logout(string username)
        {
            Response res = Service.Logout(username);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }


    }
}
