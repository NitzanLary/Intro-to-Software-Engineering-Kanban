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
    class TaskController
    {
        readonly private int MAX_TITLE = 50;
        readonly private int MAX_DESC = 300;
        private int taskNumber = 1;
        public int TaskNumber
        {
            get => taskNumber;
            //set => taskNumber += 1;
        }

        private static TaskController taskController = null;

        //This Class Is Singleton
        private TaskController()
        {

        }

        public static TaskController GetInstance()
        {
            if (taskController == null)
            {
                taskController = new TaskController();
            }
            return taskController;
        }
  
        public Response<bool> isValidTitle(string title){
            if (title.Length > MAX_TITLE || title.Equals(""))
                return Response<bool>.FromError("Task title should be at most 50 characters, not empty");
            return Response<bool>.FromValue(true);
        }

        public Response<bool> isValidDesc(string description)
        {
            if (description.Length > MAX_DESC)
                return Response<bool>.FromError("Task description should be at most 300 characters");
            return Response<bool>.FromValue(true);
        }

        public Response<Task> AddTask(string title, string description, DateTime dueDate)
        {
            Response<bool> titleValidRes = isValidTitle(title);
            if (titleValidRes.ErrorOccured)
                return Response<Task>.FromError(titleValidRes.ErrorMessage);
            Response<bool> descValidRes = isValidDesc(description);
            if(descValidRes.ErrorOccured)
                return Response<Task>.FromError(descValidRes.ErrorMessage);
            taskNumber += 1;
            return Response<Task>.FromValue(new Task(taskNumber, dueDate, title, description));
        }

        public Response UpdateTaskDueDate(Task task, DateTime NewDueDate)
        {
            if (NewDueDate < new DateTime())
                return new Response("unvalid date - already passed");
            return task.UpdateTaskDueDate(NewDueDate);
        }

        public Response UpdateTaskTitle(Task task, string title)
        {
            Response<bool> titleValidRes = isValidTitle(title);
            if (titleValidRes.ErrorOccured)
                return Response<Task>.FromError(titleValidRes.ErrorMessage);
            return task.UpdateTaskTitle(title);
    
        }

        public Response UpdateTaskDescription(Task task, string description)
        {
            Response<bool> descValidRes = isValidDesc(description);
            if (descValidRes.ErrorOccured)
                return Response<Task>.FromError(descValidRes.ErrorMessage);
            return task.UpdateTaskDescription(description);
        }
    }
}
