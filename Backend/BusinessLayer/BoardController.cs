using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
        private BoardController()
        {
            boards = new Dictionary<string, Dictionary<string, Board>>();
            taskController = TaskController.GetInstance();
        }

        public static BoardController GetInstance()
        {
            return instance == null ? new BoardController() : instance;
        }

        private Response BoardsContainsEmailAndBoard(string email, string boardName) // todo - valid input checker - add to diagram
        {
            if (!boards.ContainsKey(email))
                return Response<bool>.FromError($"boards atribute doesn't contains the given email value {email}");
            if (!boards[email].ContainsKey(boardName))
                return Response<bool>.FromError($"user {email} doesn't possess board name {boardName}");
            return new Response();
        }

        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit) 
        {
            Response validArguments = BoardsContainsEmailAndBoard(email, boardName);
            if (validArguments.ErrorOccured)
                return validArguments;
            return boards[email][boardName].limitColumn(columnOrdinal, limit);
        }

        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response validArguments = BoardsContainsEmailAndBoard(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<int>.FromError(validArguments.ErrorMessage);
            return boards[email][boardName].getColumnLimit(columnOrdinal);
        }

        public Response<string> GetColumnName(string email, string boardName, int columnOrdinal) 
        {
            Response validArguments = BoardsContainsEmailAndBoard(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<string>.FromError(validArguments.ErrorMessage);
            return boards[email][boardName].getColumnName(columnOrdinal);
        }

        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Response validArguments = BoardsContainsEmailAndBoard(email, boardName);
            if (validArguments.ErrorOccured)
                return Response<Task>.FromError(validArguments.ErrorMessage);
            Board b = boards[email][boardName];
            Response<Task> r = taskController.AddTask(title, description, dueDate, b.TaskNumber);
            if (r.ErrorOccured)
                return r;
            Task t = r.Value;
            b.AddTask(t);
            return r;
        }

        public Response UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate) { throw new NotImplementedException(); }

        public Response UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title) { throw new NotImplementedException(); }

        public Response UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description) { throw new NotImplementedException(); }

        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId) { throw new NotImplementedException(); }

        public Response<List<Task>> GetColumn(string email, string boardName, int columnOrdinal) { throw new NotImplementedException(); }

        public Response AddBoard(string email, string name) { throw new NotImplementedException(); }

        public Response RemoveBoard(string email, string name) { throw new NotImplementedException(); }

        public Response<List<Task>> InProgressTask(string email) { throw new NotImplementedException(); }
    }
}
