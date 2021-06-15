using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace Presentation.Model
{
    public class BackendController
    {
        private Service Service { get; set; }

        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }

        public BackendController(Service service)
        {
            this.Service = service;
        }

        public UserModel Login(string username, string password)
        {
            Response<User> user = Service.Login(username, password);
            if (user.ErrorOccured)
            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(this, username);
        }

        internal void Register(string username, string password)
        {
            Response res = Service.Register(username, password);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void Logout(string username)
        {
            Response res = Service.Logout(username);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void DeleteData()
        {
            Response res = Service.DeleteData();
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            Response res = Service.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal int GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Response<int> res = Service.GetColumnLimit(userEmail, creatorEmail, boardName, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.Value;
        }

        internal string GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Response<string> res = Service.GetColumnName(userEmail, creatorEmail, boardName, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.Value;
        }

        internal TaskModel AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate, DateTime creationTime, string emailAssignee)
        {
            Response<IntroSE.Kanban.Backend.ServiceLayer.Task> res = Service.AddTask(userEmail, creatorEmail, boardName, title, description, dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            // NEED TO CHANGE AND ADD TASK TO SPECIFC COLUMN, NO COLUMN ORDINAL NEEDED FOR ADD TASK, WHAT COLUMN DOES THE TASK GO INTO?
            return new TaskModel(this, title, description, dueDate, userEmail, creationTime, emailAssignee, boardName, creatorEmail, -1);  // TODO: need to implement Taskmodel
        }

        internal void UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response res = Service.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            Response res = Service.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            Response res = Service.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            Response res = Service.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        //internal IList<TaskModel> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        //{
        //    Response<IList<IntroSE.Kanban.Backend.ServiceLayer.Task>> res = Service.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);
        //    if (res.ErrorOccured)
        //    {
        //        throw new Exception(res.ErrorMessage);
        //    }
        //    IList<TaskModel> lst = new List<TaskModel>();
        //    foreach (IntroSE.Kanban.Backend.ServiceLayer.Task task in res.Value)
        //    {
        //        lst.Add(new TaskModel(this, task.Title, task.Description, task.DueDate)); // TODO: implement Task model
        //    }
        //    return lst;
        //}

        internal void AddBoard(string userEmail, string boardName)
        {
            Response res = Service.AddBoard(userEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            Response res = Service.RemoveBoard(userEmail, creatorEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal IList<TaskModel> InProgressTasks(string userEmail)
        {
            Response<IList<IntroSE.Kanban.Backend.ServiceLayer.Task>> res = Service.InProgressTasks(userEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            IList<TaskModel> lst = new List<TaskModel>();
            foreach (IntroSE.Kanban.Backend.ServiceLayer.Task task in res.Value)
            {
                lst.Add(new TaskModel(this, task.Title, task.Description, task.DueDate, userEmail, task.CreationTime, task.emailAssignee, task.boardName, task.creator, task.columnOrdinal)); // TODO: implement Task model
            }
            return lst;
        }

        internal void JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            Response res = Service.JoinBoard(userEmail, creatorEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            Response res = Service.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal IList<String> GetBoardNames(string userEmail)
        {
            Response<IList<String>> res = Service.GetBoardNames(userEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.Value;
        }

        internal IList<String> GetMyBoardNames(string userEmail)
        {
            Response<IList<String>> res = Service.GetMyBoardNames(userEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.Value;
        }

        internal IList<String> GetBoardIMemberOfNames(string userEmail)
        {
            Response<IList<String>> res = Service.GetBoardIMemberOfNames(userEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.Value;
        }

        internal void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            Response res = Service.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal, columnName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Response res = Service.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            Response res = Service.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, newColumnName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal void MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            Response res = Service.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        //internal IList<ColumnModel> GetColumns(string userEmail, string creatorEmail, string boardName)
        //{
        //    Response<IList<IntroSE.Kanban.Backend.BusinessLayer.Column>> res = Service.GetColumns(userEmail, creatorEmail, boardName);
        //    if (res.ErrorOccured)
        //    {
        //        throw new Exception(res.ErrorMessage);
        //    }
        //    return res.Value;
        //}


        //changed creator to same parameter as userEmail, maybe need to change it back
        //NOT SURE IF I DID IT RIGHT.
        internal BoardModel GetMyBoard(string userEmail, string boardName)
        {
            Response<Board> res = Service.GetBoard(userEmail, userEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            ObservableCollection<ColumnModel> columns = new ObservableCollection<ColumnModel>();
            foreach(Column c in res.Value.columns)
            {
                ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
                foreach (Task t in c.tasks)
                {
                    //NEED TO UNCOMMENT AFTER FIXING THE BUG!!!!!!!!!!!
                    //TaskModel temp_task = this.AddTask(userEmail, t.creator, t.boardName, t.Title, t.Description, t.DueDate, t.CreationTime, t.emailAssignee);
                    //tasks.Add(temp_task);
                    //NEED TO DELETE THIS LINE 283
                    tasks.Add(new TaskModel(this, t.Title, t.Description, t.DueDate, userEmail, t.CreationTime, t.emailAssignee, t.boardName, t.creator, t.columnOrdinal));
                }
                //TODO!!!!!!!! NEED TO CHANGE TO REAL NAME!!!!!!!!!!!!!!!!!@@@@@@@@@@@@@@@ <--------
                columns.Add(new ColumnModel(this, c.name, tasks, c.creator, c.boardName, c.columnOrdinal, c.maxTasks)); 
            }
            return new BoardModel(this, res.Value.name, res.Value.creator, columns, userEmail);
        }

        internal BoardModel GetAssignedBoard(string userEmail, string boardName)
        {
            Response<Board> res = Service.GetBoard(userEmail, null, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            ObservableCollection<ColumnModel> columns = new ObservableCollection<ColumnModel>();
            foreach (Column c in res.Value.columns)
            {
                ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
                foreach (Task t in c.tasks)
                {
                    //NEED TO UNCOMMENT AFTER FIXING THE BUG!!!!!!!!!!!
                    TaskModel temp_task = this.AddTask(userEmail, t.creator, t.boardName, t.Title, t.Description, t.DueDate, t.CreationTime, t.emailAssignee);
                    tasks.Add(temp_task);
                    //NEED TO DELETE THIS LINE 283
                    //tasks.Add(new TaskModel(this, t.Title, t.Description, t.DueDate, userEmail, t.CreationTime, t.emailAssignee, t.boardName, t.creator, t.columnOrdinal));
                }
                //TODO!!!!!!!! NEED TO CHANGE TO REAL NAME!!!!!!!!!!!!!!!!!@@@@@@@@@@@@@@@ <--------
                columns.Add(new ColumnModel(this, c.name, tasks, c.creator, c.boardName, c.columnOrdinal, c.maxTasks));
            }
            return new BoardModel(this, res.Value.name, res.Value.creator, columns, userEmail);
        }
    }
}

