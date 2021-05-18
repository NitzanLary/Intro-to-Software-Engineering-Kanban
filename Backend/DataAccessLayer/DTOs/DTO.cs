using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal abstract class DTO
    {
        public const string CreatorColumnName = "BoardCreator";
        public const string BoardNameColumnName = "BoardName";

        protected DALController _controller;
        protected DTO(DALController controller)
        {
            _controller = controller;
        }

    }
}
