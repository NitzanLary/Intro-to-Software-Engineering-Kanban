using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    public abstract class DTO
    {
        protected DALController _controller;
        protected DTO(DALController controller)
        {
            _controller = controller;
        }

    }
}
