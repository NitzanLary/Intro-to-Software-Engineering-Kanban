using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Board
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string name;
        public string Name
        {
            get => name;
            private set => name = value;
        }

        private string creator;
        public string Creator
        {
            get => creator;
            private set => creator = value;
        }

        private readonly List<Column> columns; // backlogs , inProgress, done (generic updatable)
        public List<Column> Columns
        {
            get => columns;
        }

        private BoardDTO dto;
        public BoardDTO DTO
        {
            get => dto;
            private set => dto = value;
        }

        public Board(string name, String creator)
        {
            Name = name;
            Creator = creator;
            columns = new List<Column> { new Column("backlog", creator, Name, 0), new Column("in progress", creator, Name, 1), new Column("done", creator, Name, 2) };
            dto = new BoardDTO(creator, name, new List<string>(), columns.Select(col => col.DTO).ToList());
            dto.Insert();
            dto.InsertNewBoardMember(creator);
        }

        public Board(BoardDTO boardDTO)
        {
            Name = boardDTO.Boardname;
            Creator = boardDTO.Creator;
            columns = boardDTO.Columns.Select((col) => new Column(col)).ToList();
            dto = boardDTO;
        }

        // pre condition: columnOrdinal < DONE_COLUMN
        public MFResponse AdvanceTask(Task task, int columnOrdinal)
        {
            int taskId = task.ID;
            try
            {
                Columns[columnOrdinal + 1].AddTask(task);
                Columns[columnOrdinal].RemoveTask(task);
                task.DTO.ColumnOrdinal++;
            }
            catch(Exception e)
            {
                return new MFResponse(e.Message);
            }
            return new MFResponse();
        }

        public MFResponse LimitColumn(int columnOrdinal, int limit)
        {
            if (columnOrdinal >= columns.Count || columnOrdinal < 0)
                return new MFResponse("there is no such column number");
            Columns[columnOrdinal].MaxTasks = limit;
            return new MFResponse();
    }

    public MFResponse<int> GetColumnLimit(int columnOrdinal)
        {
            if (columnOrdinal >= columns.Count || columnOrdinal < 0)
                return MFResponse<int>.FromError("there is no such column number");
            return MFResponse<int>.FromValue(Columns[columnOrdinal].MaxTasks);
        }

        public MFResponse<string> GetColumnName(int columnOrdinal)
        {
            if (columnOrdinal >= columns.Count || columnOrdinal < 0)
                return MFResponse<string>.FromError("there is no such column number");
            return MFResponse<string>.FromValue(columns[columnOrdinal].Name);
        }

        internal MFResponse<Task> AddTask(DateTime dueDate, string title, string description, string userEmail)
        {
            Task task;
            try
            {
                task = Columns[0].AddTask(dueDate, title, description, userEmail);
            }
            catch(Exception a)
            {
                return MFResponse<Task>.FromError(a.Message);
            }
            return MFResponse<Task>.FromValue(task);
        }

        internal MFResponse<IList<Task>> GetColumn(int columnOrdinal)
        {
            return MFResponse<IList<Task>>.FromValue(Columns[columnOrdinal].Tasks);
        }

        internal MFResponse<IList<Column>> getColumns()
        {
            return MFResponse<IList<Column>>.FromValue(Columns);
        }

        internal MFResponse AddColumn(int columnOrdinal, string columnName)
        {
            if (columnOrdinal > Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not insert to index {columnOrdinal}, max {Columns.Count}");
            for (int i = columnOrdinal; i < Columns.Count; i++)
                Columns[i].ColumnOrdinal++;
            Columns.Insert(columnOrdinal, new Column(columnName, Creator, Name, columnOrdinal));
            return new MFResponse();
        }

        internal MFResponse RemoveColumn(int columnOrdinal)
        {
            if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not remove column in index {columnOrdinal}, max {Columns.Count}");
            if (Columns.Count <= 2)
                return new MFResponse($"Can not remove any columns, the minimum that is possible is {Columns.Count}");
            Column srcCol = Columns[columnOrdinal];
            try
            {
                Column destCol;
                IList<Task> tasks = srcCol.Tasks;

                if (columnOrdinal == 0)
                    destCol = Columns[1];
                else
                    destCol = Columns[columnOrdinal - 1];

                destCol.AddTasks(tasks); // throws an exeption if tasks exceeded the max limit
                Columns.RemoveAt(columnOrdinal);
                for (int i = columnOrdinal; i < Columns.Count; i++)
                    Columns[i].ColumnOrdinal = i;
            }
            catch (Exception e)
            {
                return new MFResponse(e.Message);
            }
            return new MFResponse();
        }

        //internal MFResponse RemoveColumn(int columnOrdinal)
        //{
        //    if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
        //        return new MFResponse($"Can not remove column in index {columnOrdinal}, max {Columns.Count}");
        //    if (Columns.Count < 2)
        //        return new MFResponse($"Can not remove any columns, the minimum that is possible is {Columns.Count}");
        //    Column srcCol = Columns[columnOrdinal];
        //    IList<Task> tasks = srcCol.Tasks;
        //    if (columnOrdinal == 0)
        //    {
        //        Column destCol = Columns[1];
        //        if (destCol.MaxTasks < (destCol.Tasks.Count + srcCol.Tasks.Count))
        //            return new MFResponse("tasks exceeded the limit");
        //        Columns.RemoveAt(0);
        //        foreach (Task task in tasks)
        //            Columns[1].AddTask(task);
        //        for (int i = 0; i < Columns.Count; i++)
        //            Columns[i].ColumnOrdinal = i;
        //    }
        //    else
        //    {
        //        Column destCol = Columns[columnOrdinal - 1];
        //        if (destCol.MaxTasks < (destCol.Tasks.Count + srcCol.Tasks.Count))
        //            return new MFResponse("tasks exceeded the limit");
        //        Columns.RemoveAt(columnOrdinal);
        //        foreach (Task task in tasks)
        //            Columns[columnOrdinal - 1].AddTask(task);
        //        for (int i = columnOrdinal; i < Columns.Count; i++)
        //            Columns[i].ColumnOrdinal = i;
        //    }
        //    return new MFResponse();
        //}

        internal MFResponse RenameColumn(int columnOrdinal, string newColumnName)
        {
            if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not rename column in index {columnOrdinal}, max {Columns.Count}");
            Columns[columnOrdinal].Name = newColumnName;
            return new MFResponse();
        }

        internal MFResponse MoveColumn(int columnOrdinal, int shiftSize)
        {
            if(columnOrdinal >= Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not move column column in index {columnOrdinal}, max {Columns.Count}");
            int loc = columnOrdinal + shiftSize;
            if(loc >= Columns.Count || loc < 0)
                return new MFResponse($"Can not move column to index {columnOrdinal + shiftSize}");
            Column col = Columns[columnOrdinal];
            if (col.Tasks.Count > 0)
                return new MFResponse($"Only empty columns can be moved");
            Columns.RemoveAt(columnOrdinal);
            Columns.Insert(loc, col);
            return new MFResponse();
        }

        //public Response advanceTask(int taskId, int columnOrd)
        //{
        //    if (!containsTask(taskId))
        //        return new Response("Task dose not exist");
        //    if (Columns[2].ContainsKey(taskId))
        //        return new Response("Task is already done");
        //    if (!Columns[columnOrd].ContainsKey(taskId))
        //        return new Response("Task do not exist in this column");
        //    if (Columns[0].ContainsKey(taskId))
        //    {
        //        if (Columns[1].Keys.Count == maxInProgress)
        //            return new Response("Can not advance Task, 'In Progress' column got to its maximum limit");
        //        Task task = Columns[0][taskId];
        //        Columns[0].Remove(taskId);
        //        Columns[1].Add(taskId, task);
        //    }
        //    else if (Columns[1].ContainsKey(taskId))
        //    {
        //        if (Columns[2].Keys.Count == MaxDone)
        //            return new Response("Can not advance Task, 'done' column got to its maximum limit");
        //        Task task = Columns[1][taskId];
        //        Columns[1].Remove(taskId);
        //        Columns[2].Add(taskId, task);
        //    }
        //    return new Response();
        //}

        //public Response<Dictionary<int, Task>> getColumn(int columnOrdinal)
        //{
        //    if (columnOrdinal > 2 || columnOrdinal < 0)
        //        return Response<Dictionary<int, Task>>.FromError("there is no such column number");
        //    return Response<Dictionary<int, Task>>.FromValue(Columns[columnOrdinal]);

        //}


        //private bool containsTask(int taskId)
        //{
        //    bool flag = false;
        //    foreach(Dictionary<int, Task> dict in Columns)
        //    {
        //        if (dict.ContainsKey(taskId))
        //            flag = true;
        //    }
        //    return flag;
        //}

        //public int getNumOfTasks()
        //{
        //    int backlogsTaskNum = Columns[0].Count;
        //    int inProgressTaskNum = Columns[1].Count;
        //    int doneTaskNum = Columns[2].Count;
        //    return backlogsTaskNum + inProgressTaskNum + doneTaskNum;
        //}

        //public Response<Dictionary<int, Task>> getInProgess()
        //{
        //    return Response<Dictionary<int, Task>>.FromValue(Columns[1]);
        //}

        //public Response AddTask(Task task)
        //{
        //    if (Columns[0].Count == MaxBacklogs)
        //        return new Response("Can not add Task, backlogs column got to its maximum limit");
        //    Columns[0].Add(task.ID, task);
        //    return new Response();
        //}


        //private int maxBacklogs;
        //public int MaxBacklogs
        //{
        //    get => maxBacklogs;
        //    set => maxBacklogs = value;
        //}
        //private int maxInProgress;
        //public int MaxInProgress
        //{
        //    get => maxInProgress;
        //    set => maxInProgress = value;
        //}
        //private int maxDone;
        //public int MaxDone
        //{
        //    get => maxDone;
        //    set => maxDone = value;
        //}
    }
}

