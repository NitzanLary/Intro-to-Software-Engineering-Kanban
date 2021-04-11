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
        private static int taskNumber = 0;
        public static int TaskNumber
        {
            get => taskNumber;
            set => taskNumber += 1;
        }

        // nitzan is doing share
        public Board(string name, int maxBacklogs, int maxInProgress)
        {
            this.name = name;
            this.maxBacklogs = maxBacklogs;
            this.maxInProgress = maxInProgress;
        }

        public int getNumOfTasks()
        {
            return 0;
        }

        public Response<List<Task>> getInProgess()
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

        public Response<List<Task>> getColumn(int columnOrdinal)
        {
            return null;
        }
    }
}
