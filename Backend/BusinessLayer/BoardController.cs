using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class BoardController
    {                   // email            boardName 
        private Dictionary<string, Dictionary<string, Board>> boards;
            
        public BoardController()
        {
            this.boards = new Dictionary<string, Dictionary<string, Board>>();
        }


        public void LimitColumn(String email, string boardName, int columnOrdinal, int limit) { throw new NotImplementedException();}

        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal) { throw new NotImplementedException(); }

        public Response<String> GetColumnName(string email, string boardName, int columnOrdinal) { throw new NotImplementedException(); }

        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate) { throw new NotImplementedException(); }

        public void UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate) { throw new NotImplementedException(); }

        public void UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title) { throw new NotImplementedException(); }

        public void UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description) { throw new NotImplementedException(); }

        public void AdvanceTask(string email, string boardName, int columnOrdinal, int taskId) { throw new NotImplementedException(); }

        public Response<List<Task>> GetColumn(string email, string boardName, int columnOrdinal) { throw new NotImplementedException(); }

        public void AddBoard(string email, string name) { throw new NotImplementedException(); }

        public void RemoveBoard(string email, string name) { throw new NotImplementedException(); }

        public Response<List<Task>> InProgressTask(string email) { throw new NotImplementedException(); }
    }
}
