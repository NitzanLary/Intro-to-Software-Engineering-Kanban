using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class TaskController
    {
        readonly private int MAX_TITLE = 50;
        readonly private int MAX_DESC = 300;

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

        

  

        public Response AddTask(string title, string description, DateTime dueDate, int taskID)
        {
            throw new NotImplementedException();
        }

        public Response UpdateTaskDueDate(Task task, DateTime NewDueDate)
        {
            throw new NotImplementedException();
        }

        public Response UpdateTaskTitle(Task task, string title)
        {
            throw new NotImplementedException();
        }

        public Response UpdateTaskDescription(Task task, string description)
        {
            throw new NotImplementedException();
        }
    }
}
