using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class ColumnModel : NotifiableModelObject
    {
        //explain please...
        private ObservableCollection<TaskModel> tasks;
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

        private int _maxTasks;
        public int MaxTasks
        {
            get => _maxTasks;
            set
            {
                this._maxTasks = value;
                RaisePropertyChanged("MaxTasks");
            }
        }

        //Not sure about the parameters.. I am tired.
        public ColumnModel(BackendController controller, string name, ObservableCollection<TaskModel> tasks, string creator, string boardName, int columnOrdinal, int maxTasks) : base(controller)
        {
            this.Name = name;
            this.tasks = tasks;
            this.BoardName = boardName;
            this.Creator = creator;
            this.ColumnOrdinal = columnOrdinal;
            this.MaxTasks = maxTasks;
            
        }

        

    }


}
