using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class ColumnModel
    {
        //explain please...
        private List<TaskModel> tasks;
        public IList<TaskModel> Tasks
        {
            get => tasks.ToList();

        }


        private string name;
        public string Name
        {
            get => name;
            set => name = value;
            
        }
        //Not sure about the parameters.. I am tired.
        public ColumnModel(string name, List<TaskModel> tasks)
        {
            Name = name;
            this.tasks = tasks;
        }

    }


}
