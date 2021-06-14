using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class ColumnModel : NotifiableModelObject
    {
        //explain please...
        private List<TaskModel> tasks;
        public IList<TaskModel> Tasks
        {
            get => tasks.ToList();

        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                this._name = value;
                RaisePropertyChanged("Name");
            }
        }
    

        private string _creator;
        public string Creator
        {
            get => _creator;
            set
            {
                this._creator = value;
                RaisePropertyChanged("Creator");
            }
        }

        private string _boardName;
        public string BoardName
        {
            get => _boardName;
            set
            {
                this._boardName = value;
                RaisePropertyChanged("BoardName");
            }
        }

        private int _columnOrdinal;
        public int ColumnOrdinal
        {
            get => _columnOrdinal;
            set
            {
                this._columnOrdinal = value;
                RaisePropertyChanged("ColumnOrdinal");
            }
        }

        //Not sure about the parameters.. I am tired.
        public ColumnModel(BackendController controller, string name, List<TaskModel> tasks, string creator, string boardName, int columnOrdinal) : base(controller)
        {
            this.Name = name;
            this.tasks = tasks;
            this.BoardName = boardName;
            this.Creator = creator;
            this.ColumnOrdinal = columnOrdinal;
            
        }

    }


}
