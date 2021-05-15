using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class BoardController
    {

                        // email creator            boardName 
        private Dictionary<string, Dictionary<string, Board>> boards;
        //             email members,  board name
        private Dictionary<string, List<string>> members;


        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public BoardController()
        {
            boards = new Dictionary<string, Dictionary<string, Board>>();
            members = new Dictionary<string, List<Board>>();
        }

        /// <summary>        
        /// Checks if the "creatorEmail" is the creator of the board name
        /// </summary>
        /// <param name="creatorEmail">The email of the creator user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        internal Response isCreator(string creatorEmail, string boardName)
        {
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                return new Response("There is no board that is named: " + boardName + " that is related to this email: " + creatorEmail);
            }
            return new Response();
        }


        /// <summary>
        /// Check if user has a board in a given name, also inserting a new email address to all boards collections in case its missing
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="creatorEmail">The email address of the board's creator user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of missing board for user or invalid argments</returns>

        // TODO: check if creatorEmail is valid too
        private Response AllBoardsContainsBoardByEmail(string userEmail, string creatorEmail, string boardName) 
        {
            if (userEmail == null || creatorEmail == null || boardName == null || userEmail.Length == 0 || creatorEmail.Length == 0 || boardName.Length == 0)
                return new Response("null value given");
            if (!boards.ContainsKey(userEmail)) // TODO: ask Asaf why this is neccessary 
                boards.Add(userEmail, new Dictionary<string, Board>());
            if (!boards[userEmail].ContainsKey(boardName))
                //return Response<bool>.FromError($"user {email} doesn't possess board name {boardName}");
                return new Response($"user {email} doesn't possess board name {boardName}");
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
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit) 
        {
            //Response validArguments = AllBoardsContainsBoardByEmail(userEmail, boardName);
            //if (validArguments.ErrorOccured)
            //    return validArguments;
            if (userEmail != creatorEmail)
                return new Response("Only creator can limit columns");
            if (columnOrdinal > 2)
                return new Response("column ordinal dose not exist. max 2");
            // TODO: creatorEmail is userEmail  
            return boards[userEmail][boardName].limitColumn(columnOrdinal, limit);
        }
        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            //Response validArguments = AllBoardsContainsBoardByEmail(userEmail, boardName);
            //if (validArguments.ErrorOccured)
            //    return Response<int>.FromError(validArguments.ErrorMessage);
            if (!members[userEmail].Contains(boardName))
                return Response<int>.FromError("The user is not a board member");
            if (columnOrdinal > 2)
                return Response<int>.FromError("column ordinal dose not exist. max 2");

            return boards[userEmail][boardName].getColumnLimit(columnOrdinal);
        }



        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal) 
        {
            //Response validArguments = AllBoardsContainsBoardByEmail(userEmail, boardName);
            //if (validArguments.ErrorOccured)
            //{
            //    log.Debug(validArguments.ErrorMessage);
            //    return Response<string>.FromError(validArguments.ErrorMessage);
            //}
            if (!members[userEmail].Contains(boardName))
            {
                log.Debug("The user is not a board member");
                return Response<string>.FromError("The user is not a board member");
            }
            if (columnOrdinal > 2)
                return Response<string>.FromError("column ordinal dose not exist. max 2");
            return boards[userEmail][boardName].getColumnName(columnOrdinal);
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
        
        // TODO: CHANGE !
        public Response<Task> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            if (title == null || title.Length == 0) // || description == null
                return Response<Task>.FromError("empty string given");
            if (!members[userEmail].Contains(boardName))
            {
                log.Debug("The user is not a board member");
                return Response<Task>.FromError("The user is not a board member");
            }
            Board b = boards[userEmail][boardName];
            Response<Task> r = taskController.AddTask(title, description, dueDate); //TODO: change to Column - fix throw nitzan
            if (r.ErrorOccured)
            {
                log.Warn(r.Value);
                return r;
            }
            Task t = r.Value;
            Response res = b.AddTask(t);
            if (res.ErrorOccured)
                return Response<Task>.FromError(res.ErrorMessage);
            return r;
        }
        /// <summary>
        /// Returns a task from by id given a specific column and board name.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response with the value of the task, The response should contain a error message in case of an error</returns>

        //TODO: change to Column - fix throw nitzan
        private Response<Task> TaskGetter(string email, string boardName, int columnOrdinal, int taskId) // todo - update in the diagram
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<Task>.FromError(validArguments.ErrorMessage);
            Board b = boards[email][boardName];
            Response<Dictionary<int, Task>> res = b.getColumn(columnOrdinal);
            if (res.ErrorOccured)
                return Response<Task>.FromError(res.ErrorMessage);
            Dictionary<int, Task> col = res.Value;
            if (!col.ContainsKey(taskId))
                return Response<Task>.FromError($"coldn't find task id {taskId} in email {email} | board {boardName} | column {columnOrdinal}");
            return Response<Task>.FromValue(col[taskId]);
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

        // TODO throw nitzan: onlt task assignee can use this
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate) 
        {
            // TODO: check only assignee
            Response<Task> res = TaskGetter(userEmail, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                log.Error(res.ErrorMessage);
                return res;
            }
            if (columnOrdinal > 1)
                return new Response("task that is done, cnnot be change");
            return taskController.UpdateTaskDueDate(res.Value, dueDate);
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

        // TODO throw nitzan: onlt task assignee can use this
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title) 
        {
            // TODO: check only assignee

            if (title == null || title.Length == 0)
                return Response<Task>.FromError("empty string given");
            Response<Task> res = TaskGetter(email, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                log.Debug(res.ErrorMessage);
                return res;
            }
            if (columnOrdinal > 1)
                return new Response("task that is done, cnnot be change");
            return taskController.UpdateTaskTitle(res.Value, title);
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
        
        // TODO throw nitzan: onlt task assignee can use this
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description) 
        {
            // TODO: check only assignee

            if (description == null || description.Length == 0)
                return Response<Task>.FromError("empty string given");
            Response<Task> res = TaskGetter(email, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                log.Debug(res.ErrorMessage);
                return res;
            }
            if (columnOrdinal > 1)
                return new Response("task that is done, cnnot be change");

            return taskController.UpdateTaskDescription(res.Value, description);
        }
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        // TODO throw nitzan: onlt task assignee can use this
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId) 
        {
            Response validArguments = AllBoardsContainsBoardByEmail(userEmail, boardName);
            if (validArguments.ErrorOccured)
            {
                log.Debug(validArguments.ErrorMessage);
                return Response<Task>.FromError(validArguments.ErrorMessage);
            }
            if (columnOrdinal > 2)
                return Response<Task>.FromError("column ordinal dose not exist. max 2");
            Board b = boards[email][boardName];
            return b.advanceTask(taskId, columnOrdinal);
        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            //Response validArguments = AllBoardsContainsBoardByEmail(userEmail, boardName);
            //if (validArguments.ErrorOccured)
            //    return Response<IList<Task>>.FromError(validArguments.ErrorMessage);
            if (!members[userEmail].Contains(boardName))
            {
                log.Debug("The user is not a board member");
                return Response<IList<Task>>.FromError("The user is not a board member");
            }
            Board b = boards[userEmail][boardName];
            Response<Dictionary<int, Task>> res = b.getColumn(columnOrdinal);
            if (res.ErrorOccured)
                return Response<IList<Task>>.FromError(res.ErrorMessage);
            if (columnOrdinal > 2)
                return Response<IList<Task>>.FromError("column ordinal dose not exist. max 2");
            return Response<IList<Task>>.FromValue(res.Value.Values.ToList());
        }
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name) 
        {
            if (name == null || email == null || name.Length == 0 || email.Length == 0)
                return new Response("null value given");
            if (boards[email].ContainsKey(name))
                return new Response($"user {email} already has board named {name}");
            boards[email].Add(name, new Board(name));
            return new Response();
        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string name) 
        {
            //Response validArguments = AllBoardsContainsBoardByEmail(userEmail, name);
            //if (validArguments.ErrorOccured)
            //{
            //    log.Debug(validArguments.ErrorMessage);
            //    return Response<Task>.FromError(validArguments.ErrorMessage);
            //}
            if (userEmail != creatorEmail)
            {
                log.Debug("The user is not the board creator");
                return Response<Task>.FromError("The user is not the board creator");
            }

            boards[userEmail].Remove(name);
            return new Response();
            
        }
        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        
        //TODO: complicated for now.. todo after all things fix
        public Response<IList<Task>> InProgressTask(string email) 
        {
            if (email == null || email.Length == 0)
                return Response<IList<Task>>.FromError("empty string given");
            if (!boards.ContainsKey(email))
                return Response<IList<Task>>.FromError($"boards atribute doesn't contains the given email value {email}");
            List<Task> ret = new List<Task>();
            foreach(Board b in boards[email].Values)
            {
                Response<Dictionary<int,Task>> r = b.getInProgess();
                if (r.ErrorOccured)
                    return Response<IList<Task>>.FromError(r.ErrorMessage);
                ret.AddRange(r.Value.Values);
            }
            return Response<IList<Task>>.FromValue(ret);    
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
            if (members[userEmail].Contains(boardName))
            {
                return new Response("The user is already joined to this board");
            }
        }

        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
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
            throw new NotImplementedException();
        }
    }
}
