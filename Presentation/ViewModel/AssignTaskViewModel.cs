using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class AssignTaskViewModel : NotifiableObject
    {
        private BackendController controller;
        private UserModel user;
        private BoardModel board;
        private TaskModel task;

        private string _emailAssignee;
        public string EmailAssignee { get => _emailAssignee; set { this._emailAssignee = value; RaisePropertyChanged("EmailAssignee"); } }




        public AssignTaskViewModel(UserModel user, BoardModel board, TaskModel task)
        {
            this.controller = user.Controller;
            this.user = user;
            this.board = board;
            this.task = task;

            this._emailAssignee = EmailAssignee;
        }

        public void AssignTask()
        {
            try
            {
                controller.AssignTask(user.Email, board.Creator, board.Name, task.ColumnOrdinal, task.TaskID, EmailAssignee);
                
                MessageBox.Show("Task Assign Successfully To " + EmailAssignee  + "!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Assign Task. " + e.Message);

            }
        }
    }
}
