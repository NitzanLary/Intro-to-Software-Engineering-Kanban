using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Board
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value; 
        }
        private int maxBacklogs;
        public int MaxBacklogs
        {
            get => maxInProgress;
            set => maxInProgress = value;
        }
        private int maxInProgress;
        public int MaxInProgress
        {
            get => maxInProgress;
            set => maxInProgress = value;
        }
        //private static int taskNumber = 0;
        //public static int TaskNumber
        //{
        //    get => taskNumber;
        //    set => taskNumber += 1;
        //}

        private List<Dictionary<int, Task>> columns; // backlogs , inProgress, done (generic updatable)
        public List<Dictionary<int, Task>> Columns
        {
            get => columns;
            set => columns = value;
        }

        
        public Response AddTask(Task task) // getting a new task from task controller (through boardController) and add it here to the board. asaf & rafa
        {
            throw new NotImplementedException();
        }

        public Board(string name)
        {
            this.name = name;
            //this.maxbacklogs = maxbacklogs;
            //this.maxinprogress = maxinprogress;
        }

        public int getNumOfTasks()
        {
            return 0;
        }

        public Response<Dictionary<int, Task>> getInProgess()
        {
            return null;
        }

        public Response advanceTask(int taskId)
        {
            return null;
        }

        public Response limitColumn(int columnOrdinal, int limit)
        {
            return null;
        }

        public Response<int> getColumnLimit(int columnOrdinal)
        {
            return null;
        }

        public Response<Dictionary<int, Task>> getColumn(int columnOrdinal)
        {
            return null;
        }

        internal Response<string> getColumnName(int columnOrdinal) // todo - insert to diagram
        {
            throw new NotImplementedException();
        }
    }
}
