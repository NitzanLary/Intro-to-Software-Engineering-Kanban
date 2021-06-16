using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class UserModel : NotifiableModelObject
    {
        private string _email;
        public string Email { get => _email; set { _email = value; RaisePropertyChanged("Email"); } }

        public ObservableCollection<String> MyBoardsNames { get; set; }

        public ObservableCollection<String> AssignedBoardsNames { get; set; }


        public ObservableCollection<TaskModel> InProgressTasks { get; set; }




        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.Email = email;
            this.MyBoardsNames = new ObservableCollection<String>();
            this.AssignedBoardsNames = new ObservableCollection<String>();
            this.InProgressTasks = new ObservableCollection<TaskModel>();
            if (controller.GetMyBoardNames(Email) != null)
            {
                controller.GetMyBoardNames(Email).ToList().ForEach(MyBoardsNames.Add);

            }
            if (controller.GetBoardIMemberOfNames(Email) != null)
            {
                controller.GetBoardIMemberOfNames(Email).ToList().ForEach(AssignedBoardsNames.Add);

            }
            if (controller.InProgressTasks(Email) != null)
            {
                controller.InProgressTasks(Email).ToList().ForEach(InProgressTasks.Add);

            }
            //need to had handle change of AssignedBoards

            MyBoardsNames.CollectionChanged += HandleChangeBoardNames;

            //this.InProgressTasks = new ObservableCollection<TaskModel>(controller.InProgressTasks(Email));
        }

        public void AddBoard(String BoardName)
        {
            MyBoardsNames.Add(BoardName);

        }

        public void RemoveBoard(String BoardName)
        {
            MyBoardsNames.Remove(BoardName);

        }



        private void HandleChangeBoardNames(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (String s in e.NewItems)
                {
                    Controller.AddBoard(Email, s);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (String s in e.OldItems)
                {
                    Controller.RemoveBoard(Email, Email, s);
                }
            }
        }

        private void HandleChangeInProgressTasks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TaskModel t in e.NewItems)
                {
                    Controller.AddTask(Email, t.Creator, t.BoardName, t.Title, t.Description, t.DueDate, t.CreationTime, t.EmailAssignee);
                }
            }

            
        }

        //    private void HandleChangeTasks(object sender, NotifyCollectionChangedEventArgs e)
        //    {
        //        if (e.Action == NotifyCollectionChangedAction.Move)
        //        {

        //    }


        //}
    }
}
