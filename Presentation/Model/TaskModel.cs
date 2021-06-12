using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    class TaskModel : NotifiableModelObject
    {
        string title;
        string description;
        DateTime dueDate;
        public TaskModel(BackendController controller, string title, string description, DateTime dueDate) : base(controller)
        {
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
        }
    }
}
