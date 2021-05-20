//using IntroSE.Kanban.Backend.ServiceLayer;
//using log4net;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace IntroSE.Kanban.Backend.BusinessLayer
//{
//    class TaskController
//    {
//        readonly private int MAX_TITLE = 50;
//        readonly private int MAX_DESC = 300;
//        private int taskNumber = 0;
//        public int TaskNumber
//        {
//            get => taskNumber;
//            set => taskNumber = value;
//        }

//        public TaskController()
//        {

//        }

//        /// <summary>
//        /// check title string validity 
//        /// </summary>
//        /// <param name="title">the title given</param>
//        /// <returns>A response object. The response should contain a error message in case of false</returns>
//        public Response<bool> isValidTitle(string title){
//            if (title.Length > MAX_TITLE || title.Equals(""))
//                return Response<bool>.FromError("Task title should be at most 50 characters, not empty");
//            return Response<bool>.FromValue(true);
//        }
//        /// <summary>
//        /// check description validity 
//        /// </summary>
//        /// <param name="description">the given description</param>
//        /// <returns>A response object. The response should contain a error message in case of false</returns>
//        public Response<bool> isValidDesc(string description)
//        {
//            if (description.Length > MAX_DESC)
//                return Response<bool>.FromError("Task description should be at most 300 characters");
//            return Response<bool>.FromValue(true);
//        }
//        /// <summary>
//        /// Add a new task and gives it its id.
//        /// </summary>
//        /// <param name="title">Title of the new task</param>
//        /// <param name="description">Description of the new task</param>
//        /// <param name="dueDate">The due date if the new task</param>
//        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
//        public Response<Task> AddTask(string title, string description, DateTime dueDate)
//        {
//            if (title == null || description == null)
//            {
//                return Response<Task>.FromError("argument can not be null");
//            }
//            Response<bool> titleValidRes = isValidTitle(title);
//            if (titleValidRes.ErrorOccured)
//                return Response<Task>.FromError(titleValidRes.ErrorMessage);
//            Response<bool> descValidRes = isValidDesc(description);
//            if(descValidRes.ErrorOccured)
//                return Response<Task>.FromError(descValidRes.ErrorMessage);
//            if (dueDate < DateTime.Now)
//                return Response<Task>.FromError("due Date must be later then now");
//            taskNumber += 1;
//            return Response<Task>.FromValue(new Task(taskNumber, dueDate, title, description));
//        }
//        /// <summary>
//        /// Update the due date of a task
//        /// </summary>
//        /// <param name="task">The task to be updated</param>
//        /// <param name="dueDate">The new due date of the task</param>
//        /// <returns>A response object. The response should contain a error message in case of an error</returns>
//        public Response UpdateTaskDueDate(Task task, DateTime NewDueDate)
//        {
//            if (NewDueDate < DateTime.Now)
//                return new Response("unvalid date - already passed");
//            return task.UpdateTaskDueDate(NewDueDate);
//        }
//        /// <summary>
//        /// Update task title
//        /// </summary>
//        /// <param name="task">The task to be updated</param>
//        /// <param name="title">New title for the task</param>
//        /// <returns>A response object. The response should contain a error message in case of an error</returns>
//        public Response UpdateTaskTitle(Task task, string title)
//        {
//            if (title == null)
//            {
//                return Response<Task>.FromError("argument can not be null");
//            }
//            Response<bool> titleValidRes = isValidTitle(title);
//            if (titleValidRes.ErrorOccured)
//                return Response<Task>.FromError(titleValidRes.ErrorMessage);
//            return task.UpdateTaskTitle(title);
    
//        }
//        /// <summary>
//        /// Update the description of a task
//        /// </summary>
//        /// <param name="task">The task to be updated</param>
//        /// <param name="description">New description for the task</param>
//        /// <returns>A response object. The response should contain a error message in case of an error</returns>
//        public Response UpdateTaskDescription(Task task, string description)
//        {
//            if (description == null)
//            {
//                return Response<Task>.FromError("argument can not be null");
//            }
//            Response<bool> descValidRes = isValidDesc(description);
//            if (descValidRes.ErrorOccured)
//                return Response<Task>.FromError(descValidRes.ErrorMessage);
//            return task.UpdateTaskDescription(description);
//        }
//    }
//}
