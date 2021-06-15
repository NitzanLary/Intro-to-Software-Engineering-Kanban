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


        private ObservableCollection<TaskModel> _inProgressTasks;
        public ObservableCollection<TaskModel> InProgressTasks { get => _inProgressTasks; set { this._inProgressTasks = value; RaisePropertyChanged("InProgressTasks"); } }




        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.Email = email;
            this.MyBoardsNames = new ObservableCollection<String>();
            this.AssignedBoardsNames = new ObservableCollection<String>();
            if (controller.GetMyBoardNames(Email) != null)
            {
                controller.GetMyBoardNames(Email).ToList().ForEach(MyBoardsNames.Add);

            }
            if (controller.GetBoardIMemberOfNames(Email) != null)
            {
                controller.GetBoardIMemberOfNames(Email).ToList().ForEach(AssignedBoardsNames.Add);

            }
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

        //    private void HandleChangeTasks(object sender, NotifyCollectionChangedEventArgs e)
        //    {
        //        if (e.Action == NotifyCollectionChangedAction.Move)
        //        {

        //    }


        //}
    }
}
