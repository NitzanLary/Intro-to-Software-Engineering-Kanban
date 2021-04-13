using System.Collections.Generic;
using System;
using System.Linq;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using System.Reflection;
using log4net.Config;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Service
    {
        private UserController userController;
        private BoardController boardController;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Service()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log.Info("Starting up!");

            userController = UserController.GetInstance();
            boardController = BoardController.GetInstance();
        }
        ///<summary>This method loads the data from the persistance.
        ///         You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            throw new NotImplementedException();
        }
        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            throw new NotImplementedException();
        }
        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {
            log.Info($"User {email} is trying to Register");
            Response r = userController.Register(email, password);
            if (r.ErrorOccured)
                return r; 
            return boardController.(email, password);
        }
        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            log.Info($"User {email} is trying to Login");
            Response r = userController.Login(email, password);
            if (r.ErrorOccured)
                return Response<User>.FromError(r.ErrorMessage);
            return Response<User>.FromValue(new User(email));
        }
        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            log.Info($"User {email} is trying to Logout");
            return userController.Logout(email);
        }

        private void ValidateUserLoggin(string email)
        {
            throw new NotImplementedException();
        }

        private Response IsLoggedIn(string email)
        {
            Response<bool> r = userController.isLoggedIn(email);
            if (r.ErrorOccured)
            {
                log.Debug(r.ErrorMessage);
                return r;
            }
                
            if (!r.Value)
            {
                string msg = $"User {email} is not logged in";
                log.Debug(msg);
                return new Response(msg);
            }
                
            return new Response();
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            log.Info($"User {email} is trying to LimitColumn in board {boardName}, column {columnOrdinal} with limit: {limit}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            log.Debug($"limit column successfully to {limit}");
            return boardController.LimitColumn(email, boardName, columnOrdinal, limit);
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            log.Info($"User {email} is trying to GetColumnLimit in board {boardName}, column {columnOrdinal}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return Response<int>.FromError(r.ErrorMessage);
            return boardController.GetColumnLimit(email, boardName, columnOrdinal);
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string email, string boardName, int columnOrdinal)
        {
            log.Info($"User {email} is trying to GetColumnName in board {boardName}, column {columnOrdinal}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return Response<string>.FromError(r.ErrorMessage);
            return boardController.GetColumnName(email, boardName, columnOrdinal);
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            log.Info($"User {email} is trying to AddTask: {boardName}, {title}, {description}, {dueDate}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return Response<Task>.FromError(r.ErrorMessage);

            Response<BusinessLayer.Task> rT = boardController.AddTask(email, boardName, title, description, dueDate);
            if (rT.ErrorOccured)
                return Response<Task>.FromError(rT.ErrorMessage);
            BusinessLayer.Task task = rT.Value;
            log.Debug($"task {title} added successfully to board {boardName}");
            return Response<Task>.FromValue(new Task(task.ID, task.CreationTime, task.Title, task.Description, task.DueDate));
        }


        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            log.Info($"User {email} is trying to UpdateTaskDueDate");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            return boardController.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
        }
        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            log.Info($"User {email} is trying to UpdateTaskTitle");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            return boardController.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            log.Info($"User {email} is trying to UpdateTaskDescription");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            return boardController.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
        }
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            log.Info($"User {email} is trying to AdvanceTask in board {boardName}, column {columnOrdinal}, task {taskId}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            return boardController.AdvanceTask(email, boardName, columnOrdinal, taskId);
        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string email, string boardName, int columnOrdinal)
        {
            log.Info($"User {email} is trying to GetColumn: {boardName}, {columnOrdinal}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return Response<IList<Task>>.FromError(r.ErrorMessage);
            Response<IList<BusinessLayer.Task>> returned = boardController.InProgressTask(email);
            if (returned.ErrorOccured)
            {
                log.Debug(returned.ErrorMessage);
                return Response<IList<Task>>.FromError(returned.ErrorMessage);
            }
            return ConvertBusinessToServiceTasksCollection(returned.Value);
        }
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {
            log.Info($"User {email} is trying to AddBoard: {name}");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            r = boardController.AddBoard(email, name);
            if (r.ErrorOccured)
                log.Debug(r.ErrorMessage);
            log.Info($"Borad for user {email} succesfully added");
            return r;

        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string email, string name)
        {
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return r;
            return boardController.RemoveBoard(email, name);
        }
        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string email)
        {
            log.Info($"{email} is Trying to get InProgressTasks");
            Response r = IsLoggedIn(email);
            if (r.ErrorOccured)
                return Response<IList<Task>>.FromError(r.ErrorMessage);
            Response<IList<BusinessLayer.Task>> returned = boardController.InProgressTask(email);
            if (returned.ErrorOccured)
            {
                log.Error(returned.ErrorMessage);
                return Response<IList<Task>>.FromError(returned.ErrorMessage);
            }
            return ConvertBusinessToServiceTasksCollection(returned.Value);
        }

        private Response<IList<Task>> ConvertBusinessToServiceTasksCollection(IList<BusinessLayer.Task> lst)
        {
            IList<Task> ret = new List<Task>();
            foreach(BusinessLayer.Task t in lst)
            {
                Task toAdd = new Task(t.ID, t.CreationTime, t.Title, t.Description, t.DueDate);
                ret.Add(toAdd);
            }
            return Response<IList<Task>>.FromValue(ret);
        }
    }
}
