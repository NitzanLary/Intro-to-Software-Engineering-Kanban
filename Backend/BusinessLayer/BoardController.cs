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
            
        private BoardController()
        {
            boards = new Dictionary<string, Dictionary<string, Board>>();
        }

        public static BoardController GetInstance()
        {
            return instance == null ? new BoardController() : instance;
        }

        public Response LimitColumn(String email, string boardName, int columnOrdinal, int limit) 
        {
            bool containsEmail = boards.ContainsKey(email);
            //Dictionary<string, Board> ofUser = boards[email];
            bool containsBoard = false;
            //bool containsColumn = false;
            Board boa = null;
            if (containsEmail)
            {
                containsBoard = boards[email].ContainsKey(boardName);
            }
            if (containsBoard)
            {
                boa = boards[email][boardName];
                if (columnOrdinal > 2 || columnOrdinal < 0)
                    return new Response("invalid column number");
                
            }
            
        }

        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal) { throw new NotImplementedException(); }

        public Response<string> GetColumnName(string email, string boardName, int columnOrdinal) { throw new NotImplementedException(); }

        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate) { throw new NotImplementedException(); }

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
