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

            userController = new UserController();
            boardController = new BoardController();
        }

        ///<summary>This method loads the data from the persistance.
        ///         You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            return TryAndApply(() =>
            {
                Response<List<BusinessLayer.User>> r = Response<List<BusinessLayer.User>>.FromBLResponse(userController.LoadDate());
                WriteToLog(r, "Usres Loaded");
                if (r.ErrorOccured)
                    return r;
                r.Value.ForEach(user => boardController.addNewUserToMembers(user.Email));
                Response r2 = new Response(boardController.LoadData());
                WriteToLog(r2, "Loaded data successfully");
                return r2;
            });
        }

        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            return TryAndApply(() =>
            {
                Response r = new Response(userController.DeleteData());
                if (r.ErrorOccured)
                    return r;
                Response r2 = new(boardController.DeleteData());
                WriteToLog(r2, "Data deleted successfully");
                return r2;
            });
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userEmail">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string userEmail, string password)
        {
            return TryAndApply(() =>
            {
                log.Info($"User {userEmail} is trying to Register");
                Response r = new(userController.Register(userEmail, password));
                WriteToLog(r, $"{userEmail} succesfully registered");
                if (!r.ErrorOccured)
                    boardController.addNewUserToMembers(userEmail);
                return r;
            });
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="userEmail">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string userEmail, string password)
        {
            return TryAndApplyT<User>(() =>
            {
                log.Info($"User {userEmail} is trying to Login");
                Response r = new(userController.Login(userEmail, password));
                if (r.ErrorOccured)
                    return Response<User>.FromError(r.ErrorMessage);
                WriteToLog(r, $"{userEmail} login successfully");
                return Response<User>.FromValue(new User(userEmail));
            });
        }
        /// <summary>        
        /// Log out an logged-in user. 
        /// </summary>
        /// <param name="userEmail">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string userEmail)
        {
            return TryAndApply(() =>
            {
                log.Info($"User {userEmail} is trying to Logout");
                Response r = new(userController.Logout(userEmail));
                WriteToLog(r, $"{userEmail} logged out successfully");
                return r;
            });

        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
		/// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string userEmail,string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Creates a new board for the logged-in user.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string userEmail, string boardName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a board.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns all the in-progress tasks of the logged-in user is assigned to.
        /// </summary>
        /// <param name="userEmail">Email of the logged in user</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string userEmail)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            try
            {
                Response r = IsLoggedIn(userEmail);
                if (r.ErrorOccured)
                    return Response<IList<String>>.FromError(r.ErrorMessage);
                Response<IList<String>> r2 = Response<IList<String>>.FromBLResponse(boardController.GetBoardNames(userEmail));
                if (r2.ErrorOccured)
                    return Response<IList<String>>.FromError(r2.ErrorMessage);
                WriteToLog(r2, "GetBoardNames finished successfully");
                return r2;
            }
            catch (Exception e)
            {
                return Response<IList<String>>.FromError(e.Message);
            }
        }

        public Response<IList<String>> GetMyBoardNames(string userEmail)
        {
            try
            {
                Response r = IsLoggedIn(userEmail);
                if (r.ErrorOccured)
                    return Response<IList<String>>.FromError(r.ErrorMessage);
                Response<IList<String>> r2 = Response<IList<String>>.FromBLResponse(boardController.GetMyBoardNames(userEmail));
                if (r2.ErrorOccured)
                    return Response<IList<String>>.FromError(r2.ErrorMessage);
                WriteToLog(r2, "GetNyBoardNames finished successfully");
                return r2;
            }
            catch (Exception e)
            {
                return Response<IList<String>>.FromError(e.Message);
            }
        }

        public Response<IList<String>> GetBoardIMemberOfNames(string userEmail)
        {
            try
            {
                Response r = IsLoggedIn(userEmail);
                if (r.ErrorOccured)
                    return Response<IList<String>>.FromError(r.ErrorMessage);
                Response<IList<String>> r2 = Response<IList<String>>.FromBLResponse(boardController.GetBoardIMemberOfNames(userEmail));
                if (r2.ErrorOccured)
                    return Response<IList<String>>.FromError(r2.ErrorMessage);
                WriteToLog(r2, "GetBoardIMemberOfNames finished successfully");
                return r2;
            }
            catch (Exception e)
            {
                return Response<IList<String>>.FromError(e.Message);
            }
        }

        public Response<Objects.Board> GetBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                Response r = IsLoggedIn(userEmail);
                if (r.ErrorOccured)
                    return Response<Objects.Board>.FromError(r.ErrorMessage);
                Response<Board> r2 = Response<Board>.FromBLResponse(boardController.GetBoard(userEmail, creatorEmail, boardName));
                if (r2.ErrorOccured)
                    return Response<Objects.Board>.FromError(r2.ErrorMessage);
                WriteToLog(r2, "GetBoard finished successfully");
                Objects.Board board = new Objects.Board(r2.Value);
                return Response<Objects.Board>.FromValue(board);
            }
            catch (Exception e)
            {
                return Response<Objects.Board>.FromError(e.Message);
            }
        }

      

        public Response<IList<Objects.Column>> GetColumns(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                log.Info($"{userEmail} is Trying to get all columns from board {boardName}");
                Response r = IsLoggedIn(userEmail);
                if (r.ErrorOccured)
                    return Response<IList<Objects.Column>>.FromError(r.ErrorMessage);
                Response<IList<BusinessLayer.Column>> returned = Response<IList<BusinessLayer.Column>>.FromBLResponse(boardController.getColumns(userEmail, creatorEmail, boardName));
                if (returned.ErrorOccured)
                {
                    return Response<IList<Objects.Column>>.FromError(returned.ErrorMessage);
                }
                WriteToLog(r, $"getColumns finished successfully");
                return Response<IList<Objects.Column>>.FromValue(returned.Value.Select(c => new Objects.Column(c)).ToList());
            }
            catch (Exception e)
            {
                return Response<IList<Objects.Column>>.FromError(e.Message);
            }
        }





        // ************ New code starts here ***************





        /// <summary>
        /// Adds a new column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The location of the new column. Location for old columns with index>=columnOrdinal is increased by 1 (moved right). The first column is identified by 0, the location increases by 1 for each column.</param>
        /// <param name="columnName">The name for the new columns</param>        
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Removes a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Renames a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="newColumnName">The new column name</param>        
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves a column shiftSize times to the right. If shiftSize is negative, the column moves to the left
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="shiftSize">The number of times to move the column, relativly to its current location. Negative values are allowed</param>  
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write to the log the message from a given response
        /// </summary>
        /// <param name="r"> The given response </param>
        /// <param name="msg"> The message to write if there was no error in the response </param>
        private void WriteToLog(Response r, string msg)
        {
            if (r.ErrorOccured)
                log.Error(r.ErrorMessage);
            else log.Info(msg);
        }

        /// <summary>
        /// Converts a collection of BusinessLayer Tasks into collection of ServiceLayer Tasks
        /// </summary>
        /// <param name="lst">IList of BL Tasks</param>
        /// <returns>A response object with a value set to the list of SL tasks</returns>
        private Response<IList<Task>> ConvertBusinessToServiceTasksCollection(IList<BusinessLayer.Task> lst)
        {
            return TryAndApplyT<IList<Task>>(() =>
        {
            IList<Task> ret = new List<Task>();
            foreach (BusinessLayer.Task t in lst)
            {
                Task toAdd = new Task(t.ID, t.CreationTime, t.Title, t.Description, t.DueDate, t.Assignee);
                ret.Add(toAdd);
            }
            return Response<IList<Task>>.FromValue(ret);
        });
            
        }

        private Response IsLoggedIn(string email)
        {
            try
            {
                Response<bool> r = Response<bool>.FromBLResponse(userController.isLoggedIn(email));
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
            catch (Exception e)
            {
                return Response<User>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Wrraping a given lambda with try catch and applying the function.
        /// </summary>
        /// <typeparam name="T"> The type of the Response to return </typeparam>
        /// <param name="func"> The function to apply which accepts no argiments and return Response<T>. </param>
        /// <returns> A response of type T </returns>
        private Response<T> TryAndApplyT<T>(Func<Response<T>> func)
        {
            try
            {
                return func();
            }
            catch(Exception e)
            {
                return Response<T>.FromError(e.Message);
            }
        }

        /// <summary> Same as TryAndApplyT method, but gets no generic type T in the Response. </summary>
        private Response TryAndApply(Func<Response> func)
        {
            return TryAndApplyT<Object>(() =>
            {
                Response r = func();
                if (r.ErrorOccured)
                    return Response<Object>.FromError(r.ErrorMessage);
                return Response<Object>.FromValue(null);
            });
        }

        /// <summary>
        /// Confirms that <c>userEmail</c> is logged in first, then apply the function wrraped with try - catch.
        /// </summary>
        /// /// <typeparam name="T"> The type of the Response to return </typeparam>
        /// <param name="func"> The function to apply which accepts no argiments and return Response<T>. </param>
        /// <param name="userEmail"> The user to confirm whether logged in or not </param>
        /// <returns> A response of type T</returns>
        private Response<T> ConfirmAndApply<T>(string userEmail, Func<Response<T>> func)
        {
            return TryAndApplyT<T>(() =>
            {
                Response r = IsLoggedIn(userEmail);
                if (r.ErrorOccured)
                    return Response<T>.FromError(r.ErrorMessage);
                return func();
            });
        }
    }
}
