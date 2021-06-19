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
        private ObservableCollection<TaskModel> _tasks;
        public ObservableCollection<TaskModel> Tasks { get => _tasks; set { _tasks = value; RaisePropertyChanged("Tasks"); } }
        //public IList<TaskModel> Tasks
        //{
        //    get => tasks.ToList();

        //}

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
        public ColumnModel(BackendController controller, string name, ObservableCollection<TaskModel> tasks, string creator, string boardName, int columnOrdinal, int maxTasks, string userEmail) : base(controller)
        {
            this.Name = name;
            this.Tasks = tasks;
            this.BoardName = boardName;
            this.Creator = creator;
            this.ColumnOrdinal = columnOrdinal;
            this.MaxTasks = maxTasks;
            this.UserEmail = userEmail;
            Tasks.CollectionChanged += HandleChangeTasks;


        }

        private string UserEmail; //storing this user here is an hack becuase static & singletone are not allowed.
        //NOT GOOD. SHOULDNT GET USER EMAIL AS PARAMETER!
        private void HandleChangeTasks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if(ColumnOrdinal == 0){
                    foreach (TaskModel t in e.NewItems)
                    {
                        //to put in ifErrorOccurd => RoolBack
                        TaskModel tempTask = Controller.AddTask(UserEmail, Creator, BoardName, t.Title, t.Description, t.DueDate, t.CreationTime, t.EmailAssignee);
                        t.TaskID = tempTask.TaskID;
                    }
                }
                else
                {
                    foreach(TaskModel t in e.NewItems)
                    {
                        Controller.AdvanceTask(UserEmail, Creator, BoardName, ColumnOrdinal-1, t.TaskID);
                    }
                }
            }

            
        }

        public void AddTask(TaskModel task)
        {
            Tasks.Add(task);
        }



    }


}
