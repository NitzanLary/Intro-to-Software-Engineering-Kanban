using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class BoardController
    {

        // email creator            boardName 
        private Dictionary<string, Dictionary<string, Board>> boards;
        //             email members,  board name
        private Dictionary<string, HashSet<Board>> members;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardController()
        {
            boards = new Dictionary<string, Dictionary<string, Board>>();
            members = new Dictionary<string, HashSet<Board>>();
        }

        // pre condition: members were intiialized
        internal MFResponse LoadData()
        {
            try
            {
                List<BoardDTO> dtos = new BoardDALController().SelectAllBoards();
                foreach (BoardDTO boardDTO in dtos)
                {
                    Board board = new Board(boardDTO);
                    boardDTO.BoardMembers.ForEach((user) => members[user].Add(board));
                    if (!boards.ContainsKey(boardDTO.Creator))
                        boards.Add(boardDTO.Creator, new Dictionary<string, Board>());
                    boards[boardDTO.Creator].Add(board.Name, board);
                }
            }
            catch (Exception e)
            {
                return new MFResponse(e.Message);
            }
            return new MFResponse();
        }

        public MFResponse DeleteData()
        {
            try
            {
                new BoardDALController().DeleteAllData();
            }
            catch(Exception e)
            {
                return new MFResponse(e.Message);
            }
            return new MFResponse();
        }

        /// <summary>        
        /// Checks args validities.
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="creatorEmail">The email of the creator user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response<T> object according tho the <c>apply</c> function. The response should contain a error message in case of an error</returns>
        private MFResponse CheckArgs(string userEmail, string creatorEmail, string boardName)
        {
            MFResponse r = isCreator(creatorEmail, boardName);
            if (r.ErrorOccured)
                return r;
            return isMember(userEmail, creatorEmail, boardName);
        }

        /// <summary>        
        /// Checks if 'userEmail' is a member in the board 
        /// Pre condition: there is a board named <c>boardName</c> created by <c>creatorEmail</c>
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="creatorEmail">The email of the creator user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        private MFResponse isMember(string userEmail, string creatorEmail, string boardName)
        {
            if (!members.ContainsKey(userEmail))
                return new MFResponse($"Could not find {userEmail}");
            if (!members[userEmail].Contains(boards[creatorEmail][boardName]))
                return new MFResponse($"{userEmail} is not a member in this board");
            return new MFResponse();
        }

        /// <summary>        
        /// Checks if the "creatorEmail" is the creator of the board name
        /// </summary>
        /// <param name="creatorEmail">The email of the creator user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        internal MFResponse isCreator(string creatorEmail, string boardName)
        {
            if (!boards.ContainsKey(creatorEmail))
                return new MFResponse($"{creatorEmail} has no boards");

            if (!boards[creatorEmail].ContainsKey(boardName))
                return new MFResponse("There is no board that is named: " + boardName + " that is related to this email: " + creatorEmail);
            
            return new MFResponse();
        }

        /// <summary>        
        /// add new user that registered to the to the members with empty list
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        internal void addNewUserToMembers(string userEmail)
        {
            members.Add(userEmail, new HashSet<Board>());
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit) 
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return r;
            if (userEmail != creatorEmail)
                return new MFResponse("Only creator can limit columns");
            return boards[userEmail][boardName].LimitColumn(columnOrdinal, limit);
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public MFResponse<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<int>.FromError(r.ErrorMessage);
            if (!members[userEmail].Contains(boards[creatorEmail][boardName]))
                return MFResponse<int>.FromError("The user is not a board member");
            Board b = boards[creatorEmail][boardName];
            if (columnOrdinal > b.Columns.Count)
                return MFResponse<int>.FromError("column ordinal dose not exist. max " + b.Columns.Count);

            return b.GetColumnLimit(columnOrdinal);
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public MFResponse<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal) 
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<string>.FromError(r.ErrorMessage);
            if (!members[userEmail].Contains(boards[creatorEmail][boardName]))
            {
                log.Debug("The user is not a board member");
                return MFResponse<string>.FromError("The user is not a board member");
            }
            Board b = boards[userEmail][boardName];
            if (columnOrdinal >= b.Columns.Count)
                return MFResponse<string>.FromError("column ordinal dose not exist. max " + b.Columns.Count);
            return b.GetColumnName(columnOrdinal);
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="userEmail">Email of the user. The user must be logged in.</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public MFResponse<Task> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<Task>.FromError(r.ErrorMessage);
            Board b = boards[creatorEmail][boardName];
            MFResponse<Task> task = b.AddTask(dueDate, title, description, userEmail);
            if (!task.ErrorOccured)
            {
                try
                {
                    task.Value.AttachDto(creatorEmail, boardName);
                }
                catch(Exception e)
                {
                    return MFResponse<Task>.FromError(e.Message);
                }
            }
            return task;
        }

        /// <summary>
        /// Returns a task by id from a given specific column and a board name.
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response with the value of the task, The response should contain a error message in case of an error</returns>
        private MFResponse<Task> TaskGetter(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<Task>.FromError(r.ErrorMessage);
            Board b = boards[creatorEmail][boardName];
            if (columnOrdinal >= b.Columns.Count || columnOrdinal < 0)
                return MFResponse<Task>.FromError("there is no such column number");
            try
            {
                Task t = b.Columns[columnOrdinal].GetTask(taskId);
                return MFResponse<Task>.FromValue(t);
            }
            catch(Exception e)
            {
                return MFResponse<Task>.FromError($"coldn't find task id {taskId} in email {userEmail} | board {boardName} | column {columnOrdinal}\n{e.Message}");
            }
            
        }

        /// <summary>
        /// A generic function that first check if the args are valid, and than apply the relevant function according what is needs to update
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="updateFunc">A generic function, according to the argument it received updates what is relevant </param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse UpdateTask<T>(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, Func<Task, T> updateFunc) 
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return r;
            MFResponse<Task> res = TaskGetter(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                log.Error(res.ErrorMessage);
                return res;
            }
            if (userEmail != res.Value.Assignee)
                return new MFResponse("only the assignee of the task can update");
            Board b = boards[creatorEmail][boardName];
            if (columnOrdinal == b.Columns.Count - 1)
                return new MFResponse("task that is done, cannot be change");
            try
            {
                updateFunc(res.Value);
            }
            catch(Exception e)
            {
                return new MFResponse(e.Message);
            }
            return new MFResponse();
        }
        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return UpdateTask<DateTime>(userEmail, creatorEmail, boardName, columnOrdinal, taskId, (task) => task.DueDate = dueDate);
        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        ///<param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title) 
        {
            return UpdateTask<string>(userEmail, creatorEmail, boardName, columnOrdinal, taskId, (task) => task.Title = title);
        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description) 
        {
            return UpdateTask<string>(userEmail, creatorEmail, boardName, columnOrdinal, taskId, (task) => task.Description = description);
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId) 
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return r;
            Board b = boards[creatorEmail][boardName];
            return UpdateTask<MFResponse>(userEmail, creatorEmail, boardName, columnOrdinal, taskId, (task) => b.AdvanceTask(task, columnOrdinal));
        }

        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public MFResponse<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<IList<Task>>.FromError(r.ErrorMessage);
            Board b = boards[creatorEmail][boardName];
            if (columnOrdinal >= b.Columns.Count)
                return MFResponse<IList<Task>>.FromError("column ordinal dose not exist.");
            return boards[userEmail][boardName].GetColumn(columnOrdinal);
        }

        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse AddBoard(string email, string name) 
        {
            if (name == null || email == null || name.Length == 0 || email.Length == 0)
                return new MFResponse("null value given");
            if (!boards.ContainsKey(email))
                boards.Add(email, new Dictionary<string, Board>());
            if (boards[email].ContainsKey(name))
                return new MFResponse($"user {email} already has board named {name}");
            try
            {
                Board board = new Board(name, email);
                boards[email].Add(name, board);
                members[email].Add(board);
            }
            catch(Exception e)
            {
                return new MFResponse(e.Message);
            }
            
            return new MFResponse();
        }

        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            if (userEmail != creatorEmail)
            {
                log.Debug("The user is not the board creator");
                return MFResponse<Task>.FromError("The user is not the board creator");
            }

            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return r;

            return RemoveBoardHelper(creatorEmail, boardName);
        }

        /// <summary>
        /// Removes a board to the specific user and remove it from all the members.
        /// </summary>
        /// <param name="creatorEmail">Email of the user that create the board.</param>
        /// <param boardName="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        private MFResponse RemoveBoardHelper(string creatorEmail, string boardName)
        {
            Board board = boards[creatorEmail][boardName];
            try
            {
                board.DTO.Delete();
            }
            catch(Exception e)
            {
                return new MFResponse(e.Message);
            }

            boards[creatorEmail].Remove(boardName);
            foreach(KeyValuePair<string, HashSet<Board>> entry in members)
            {
                if (entry.Value.Contains(board))
                    members[entry.Key].Remove(board);
            }
            return new MFResponse();
        }

        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// note: 'email' is in members because it was registered.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>

        //TODO: complicated for now.. todo after all things fix
        public MFResponse<IList<Task>> InProgressTask(string email) 
        {
           List<Task> tasks = new List<Task>();
            foreach(Board board in members[email])
            {
                MFResponse<IList<Task>> r = board.GetColumn(1); // 1 is the column ordinal of inProgress
                if (r.ErrorOccured)
                    return MFResponse<IList<Task>>.FromError(r.ErrorMessage);
                tasks.AddRange(r.Value.Where((task) => task.Assignee == email).ToList());
            }
            return MFResponse<IList<Task>>.FromValue(tasks);
        }

        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            MFResponse r = isCreator(creatorEmail, boardName);
            if (r.ErrorOccured)
                return r;

            if (members[userEmail].Contains(boards[creatorEmail][boardName]))
            {
                return new MFResponse("The user is already joined to this board");
            }
            Board b = boards[creatorEmail][boardName];
            try
            {
                b.DTO.InsertNewBoardMember(userEmail);
            }
            catch(Exception e)
            {
                return new MFResponse(e.Message);
            }
            members[userEmail].Add(b);
            return new MFResponse();
        }

        /// <summary>
        /// Assigns a task to a user
        /// Asumption: only task's assignee can assign other board member to the task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            return UpdateTask<string>(userEmail, creatorEmail, boardName, columnOrdinal, taskId, (task) => task.Assignee = emailAssignee);
        }

        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public MFResponse<IList<String>> GetBoardNames(string userEmail)
        {
            return MFResponse<IList<String>>.FromValue(members[userEmail].Select((b) => b.Name).ToList());
        }

        public MFResponse<IList<String>> GetMyBoardNames(string userEmail)
        {
            return MFResponse<IList<String>>.FromValue(boards[userEmail].Select((b) => b.Key).ToList());
        }

        public MFResponse<IList<String>> GetBoardIMemberOfNames(string userEmail)
        {
            IList<String> myBoards = GetMyBoardNames(userEmail).Value;
            return MFResponse<IList<String>>.FromValue(members[userEmail].Select((b) => b.Name).ToList().Where(x => !myBoards.Contains(x)).ToList());
        }

        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public MFResponse<Board> GetBoard(string userEmail, string creatorEmail, string boardName)
        {

            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<Board>.FromError(r.ErrorMessage);
            return MFResponse<Board>.FromValue(boards[creatorEmail][boardName]);
        }


        public MFResponse<IList<Column>> getColumns(string userEmail, string creatorEmail, string boardName)
        {
            MFResponse r = CheckArgs(userEmail, creatorEmail, boardName);
            if (r.ErrorOccured)
                return MFResponse<IList<Column>>.FromError(r.ErrorMessage);
            Board b = boards[creatorEmail][boardName];
            return b.getColumns();
        }


        //private Response<Task> TaskGetter(string email, string creatorEmail, string boardName, int columnOrdinal, int taskId) // todo - update in the diagram
        //{
        //    Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
        //    if (validArguments.ErrorOccured)
        //        return Response<Task>.FromError(validArguments.ErrorMessage);
        //    Board b = boards[email][boardName];
        //    Response<Dictionary<int, Task>> res = b.getColumn(columnOrdinal);
        //    if (res.ErrorOccured)
        //        return Response<Task>.FromError(res.ErrorMessage);
        //    Dictionary<int, Task> col = res.Value;
        //    if (!col.ContainsKey(taskId))
        //        return Response<Task>.FromError($"coldn't find task id {taskId} in email {email} | board {boardName} | column {columnOrdinal}");
        //    return Response<Task>.FromValue(col[taskId]);
        //}


        /// <summary>
        /// Check if user has a board in a given name, also inserting a new email address to all boards collections in case its missing
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="creatorEmail">The email address of the board's creator user</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of missing board for user or invalid argments</returns
        // TODO: check if creatorEmail is valid too
        //private Response AllBoardsContainsBoardByEmail(string userEmail, string creatorEmail, string boardName) 
        //{
        //    if (userEmail == null || creatorEmail == null || boardName == null || userEmail.Length == 0 || creatorEmail.Length == 0 || boardName.Length == 0)
        //        return new Response("null value given");
        //    if (!boards.ContainsKey(userEmail)) // TODO: ask Asaf why this is neccessary 
        //        boards.Add(userEmail, new Dictionary<string, Board>());
        //    if (!boards[userEmail].ContainsKey(boardName))
        //        //return Response<bool>.FromError($"user {email} doesn't possess board name {boardName}");
        //        return new Response($"user {email} doesn't possess board name {boardName}");
        //    return new Response();
        //}


    }
}
