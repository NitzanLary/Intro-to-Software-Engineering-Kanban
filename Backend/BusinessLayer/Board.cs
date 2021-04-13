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
            get => maxBacklogs;
            set => maxBacklogs = value;
        }
        private int maxInProgress;
        public int MaxInProgress
        {
            get => maxInProgress;
            set => maxInProgress = value;
        }
        private int maxDone;
        public int MaxDone
        {
            get => maxDone;
            set => maxDone = value;
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
            if (Columns[0].Count == MaxBacklogs)
                return new Response("Can not add Task, backlogs column got to its maximum limit");
            Columns[0].Add(task.ID, task);
            return new Response();
        }

        public Board(string name)
        {
            this.name = name;
            this.MaxBacklogs = int.MaxValue;
            this.MaxInProgress = int.MaxValue;
            this.MaxDone = int.MaxValue;
        }

        public int getNumOfTasks()
        {
            int backlogsTaskNum = Columns[0].Count;
            int inProgressTaskNum = Columns[1].Count;
            int doneTaskNum = Columns[2].Count;
            return backlogsTaskNum + inProgressTaskNum + doneTaskNum;
        }

        public Response<Dictionary<int, Task>> getInProgess()
        {
            return Response<Dictionary<int, Task>>.FromValue(Columns[1]);
        }

        public Response advanceTask(int taskId)
        {
            if (!containsTask(taskId))
                return new Response("Task dose not exist");
            if (Columns[2].ContainsKey(taskId))
                return new Response("Task is already done");
            
            if (Columns[0].ContainsKey(taskId))
            {
                if (Columns[1].Keys.Count == maxInProgress)
                    return new Response("Can not advance Task, In Progress column got to its maximum limit");
                Task task = Columns[0][taskId];
                Columns[0].Remove(taskId);
                Columns[1].Add(taskId, task);
            }
            if (Columns[1].ContainsKey(taskId))
            {
                Task task = Columns[1][taskId];
                Columns[1].Remove(taskId);
                Columns[2].Add(taskId, task);
            }
            return new Response();
        }

        public Response limitColumn(int columnOrdinal, int limit)
        {
            if (columnOrdinal > 2 || columnOrdinal < 0)
                return new Response("there is no such column number");
            if (Columns[columnOrdinal].Count > limit)
                return new Response("There are already more tasks in this column from the limit you put");
            if (columnOrdinal == 0)
                MaxBacklogs = limit;
            if (columnOrdinal == 1)
                MaxInProgress = limit;
            if (columnOrdinal == 2)
                MaxDone = limit;
            return new Response();
        }

        public Response<int> getColumnLimit(int columnOrdinal)
        {
            if (columnOrdinal == 0)
                return Response<int>.FromValue(MaxBacklogs);
            if (columnOrdinal == 1)
                return Response<int>.FromValue(MaxInProgress);
            if (columnOrdinal == 2)
                return Response<int>.FromValue(MaxDone);
            return Response<int>.FromError("there is no such column number");
        }

        public Response<Dictionary<int, Task>> getColumn(int columnOrdinal)
        {
            if (columnOrdinal > 2 || columnOrdinal < 0)
                return Response<Dictionary<int, Task>>.FromError("there is no such column number");
            return Response<Dictionary<int, Task>>.FromValue(Columns[columnOrdinal]);

        }

        internal Response<string> getColumnName(int columnOrdinal) // todo - insert to diagram
        {
            if (columnOrdinal == 0)
                return Response<string>.FromValue("Backlogs");
            if (columnOrdinal == 1)
                return Response<string>.FromValue("InProgress");
            if (columnOrdinal == 2)
                return Response<string>.FromValue("Done");
            return Response<string>.FromError("there is no such column number");

        }

        private bool containsTask(int taskId)
        {
            bool flag = false;
            foreach(Dictionary<int, Task> dict in Columns)
            {
                if (dict.ContainsKey(taskId))
                    flag = true;
            }
            return flag;
        }

    }
}
