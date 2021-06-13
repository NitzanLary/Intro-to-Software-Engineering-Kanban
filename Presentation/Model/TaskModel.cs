using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class TaskModel : NotifiableModelObject
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                this._title = value;
                RaisePropertyChanged("Title");
                //need to figure out how to pass those params

                //Controller.UpdateTaskTitle(UserEmail, )
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                this._description = value;
                RaisePropertyChanged("Description");
                //need to figure out how to pass those params

                //Controller.UpdateTaskDescription(UserEmail, UserEmail, boardName, columnOrdinal, taskID, Description);
            }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                this._dueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }


        private DateTime _creationTime;
        public DateTime CreationTime
        {
            get => _creationTime;
            set
            {
                this._creationTime = value;
            }
        }

        private string _emailAssignee;
        public string EmailAssignee
        {
            get => _emailAssignee;
            set
            {
                this._emailAssignee = value;
                RaisePropertyChanged("EmailAssignee");
            }
        }

        //private string boardName;
        //private int columnOrdinal;
        //private int taskID;

        private string UserEmail;
        
        public TaskModel(BackendController controller, string title, string description, DateTime dueDate, string user_email, DateTime creationTime, string emailAssignee) : base(controller)
        {
            this._title = title;
            this._description = description;
            this._dueDate = dueDate;
            this.UserEmail = user_email;
            this.CreationTime = creationTime;
            this.EmailAssignee = emailAssignee;
        }
    }
}
