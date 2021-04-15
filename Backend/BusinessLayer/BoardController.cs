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
        private static BoardController instance;

                        // email            boardName 
        private Dictionary<string, Dictionary<string, Board>> boards;

        private TaskController taskController;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private BoardController()
        {
            boards = new Dictionary<string, Dictionary<string, Board>>();
            taskController = TaskController.GetInstance();
        }

        public static BoardController GetInstance()
        {
            return instance == null ? new BoardController() : instance;
        }

        private Response AllBoardsContainsBoardByEmail(string email, string boardName) // todo - valid input checker - add to diagram
        {
            if (!boards.ContainsKey(email))
                boards.Add(email, new Dictionary<string, Board>());
            if (!boards[email].ContainsKey(boardName))
                //return Response<bool>.FromError($"user {email} doesn't possess board name {boardName}");
                return new Response($"user {email} doesn't possess board name {boardName}");
            return new Response();
        }

        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit) 
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
                return validArguments;
            return boards[email][boardName].limitColumn(columnOrdinal, limit);
        }

        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<int>.FromError(validArguments.ErrorMessage);
            return boards[email][boardName].getColumnLimit(columnOrdinal);
        }

        public Response<string> GetColumnName(string email, string boardName, int columnOrdinal) 
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
            {
                log.Debug(validArguments.ErrorMessage);
                return Response<string>.FromError(validArguments.ErrorMessage);
            }
            return boards[email][boardName].getColumnName(columnOrdinal);
        }

        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<Task>.FromError(validArguments.ErrorMessage);
            Board b = boards[email][boardName];
            Response<Task> r = taskController.AddTask(title, description, dueDate);
            if (r.ErrorOccured)
            {
                log.Warn(r.Value);
                return r;
            }
            Task t = r.Value;
            b.AddTask(t);
            return r;
        }

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

        public Response UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate) 
        {
            Response<Task> res = TaskGetter(email, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                log.Error(res.ErrorMessage);
                return res;
            }
            if (columnOrdinal > 1)
                return new Response("task that is done, cnnot be change");
            return taskController.UpdateTaskDueDate(res.Value, dueDate);
        }

        public Response UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title) 
        {
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

        public Response UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description) 
        {
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

        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId) 
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
            {
                log.Debug(validArguments.ErrorMessage);
                return Response<Task>.FromError(validArguments.ErrorMessage);
            }
            Board b = boards[email][boardName];
            return b.advanceTask(taskId, columnOrdinal);
        }

        public Response<IList<Task>> GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<IList<Task>>.FromError(validArguments.ErrorMessage);
            Board b = boards[email][boardName];
            Response<Dictionary<int, Task>> res = b.getColumn(columnOrdinal);
            if (res.ErrorOccured)
                return Response<IList<Task>>.FromError(res.ErrorMessage);
            return Response<IList<Task>>.FromValue(res.Value.Values.ToList());
        }

        public Response AddBoard(string email, string name) 
        {
            if (!AllBoardsContainsBoardByEmail(email,name).ErrorOccured)
                return new Response($"user {email} already has board named {name}");
            boards[email].Add(name, new Board(name));
            return new Response();
        }

        public Response RemoveBoard(string email, string name) 
        {
            Response validArguments = AllBoardsContainsBoardByEmail(email, name);
            if (validArguments.ErrorOccured)
            {
                log.Debug(validArguments.ErrorMessage);
                return Response<Task>.FromError(validArguments.ErrorMessage);
            }
                
            boards[email].Remove(name);
            return new Response();
            
        }

        public Response<IList<Task>> InProgressTask(string email) 
        {
            if (!boards.ContainsKey(email))
                return Response<IList<Task>>.FromError($"boards atribute doesn't contains the given email value {email}");
            List<Task> ret = new List<Task>();
            foreach(Board b in boards[email].Values)
            {
                Response<Dictionary<int,Task>> r = b.getInProgess();
                if (r.ErrorOccured)
                    return Response<IList<Task>>.FromError(r.ErrorMessage);
                ret.AddRange(r.Value.Values);
                Console.WriteLine("-------------------------------" + r.Value.Count + "-------------------------------" );
            }
            return Response<IList<Task>>.FromValue(ret);    
        }
    }
}
