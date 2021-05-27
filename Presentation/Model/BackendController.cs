using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    class BackendController
    {
        private Service Service { get; set; }

        public BackendController(Service service)
        {
            this.Service = service;
            Service.LoadData();
        }


    }
}
