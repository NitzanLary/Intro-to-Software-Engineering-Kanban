using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

                Controller.UpdateTaskTitle(UserEmail, Creator, BoardName, ColumnOrdinal, TaskID, Title);
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

                Controller.UpdateTaskDescription(UserEmail, Creator, BoardName, ColumnOrdinal, TaskID, Description);
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
                Controller.UpdateTaskDueDate(UserEmail, Creator, BoardName, ColumnOrdinal, TaskID, DueDate);
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

        private string _boardName;
        public string BoardName
        {
            get => _boardName;
            //set
            //{
            //    this._boardName = value;
            //    RaisePropertyChanged("BoardName");
            //}
        }

        private string _creator;
        public string Creator
        {
            get => _creator;
            //set
            //{
            //    this._creator = value;
            //    RaisePropertyChanged("Creator");
            //}
        }

        private int _taskID;
        public int TaskID
        {
            get => _taskID;
            set
            {
                this._taskID = value;
                RaisePropertyChanged("TaskID");
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

        //private string boardName;
        //private int columnOrdinal;
        //private int taskID;

        private string UserEmail;
        
        public TaskModel(BackendController controller, string title, string description, DateTime dueDate, string user_email, DateTime creationTime, string emailAssignee, string boardName, string creator, int columnOrdinal) : base(controller)
        {
            this._title = title;
            this._description = description;
            this._dueDate = dueDate;
            this.UserEmail = user_email;
            this.CreationTime = creationTime;
            this.EmailAssignee = emailAssignee;
            this._boardName = boardName;
            this._creator = creator;
            this._columnOrdinal = columnOrdinal;
            this._taskID = TaskID;
        }

        public SolidColorBrush BorderColor
        {
            get
            {
                return new SolidColorBrush(EmailAssignee == UserEmail ? Colors.Blue : Colors.Transparent);
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                if(DateTime.Now > DueDate)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else
                {
                    double res = ((DateTime.Now.Subtract(CreationTime)).Days / (DueDate.Subtract(CreationTime).Days));
                    Boolean timeElapsedOver75Per = res > 0.75;
                    if(timeElapsedOver75Per){
                        return new SolidColorBrush(Colors.Orange);
                    }
                    return new SolidColorBrush(Colors.Transparent);
                }
               
            }
        }
    }
}
