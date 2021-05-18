using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal abstract class DTO
    {
        public const string IDColumnName = "ID";
        protected DALController _controller;
        public long Id { get; set; } = -1;
        protected DTO(DALController controller)
        {
            _controller = controller;
        }

    }
}
