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
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            //set
            //{
            //    this._description = value;
            //    RaisePropertyChanged("Description");
            //    Controller.UpdateTaskDescription(UserEmail, UserEmail, boardName, columnOrdinal, taskID, Description);
            //}
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                this.DueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }


        //private string boardName;
        //private int columnOrdinal;
        //private int taskID;

        private string UserEmail;
        
        public TaskModel(BackendController controller, string title, string description, DateTime dueDate, string user_email) : base(controller)
        {
            this._title = title;
            this._description = description;
            this._dueDate = dueDate;
            this.UserEmail = user_email;
        }
    }
}
