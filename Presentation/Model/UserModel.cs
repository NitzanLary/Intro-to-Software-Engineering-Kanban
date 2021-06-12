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

        public ObservableCollection<String> BoardsNames { get; set; }


        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.Email = email;
            this.BoardsNames = new ObservableCollection<String>();
            if (controller.GetBoardNames(Email) != null)
            {
                controller.GetBoardNames(Email).ToList().ForEach(BoardsNames.Add);

            }
            BoardsNames.CollectionChanged += HandleChange;


        }

        public void AddBoard(String BoardName)
        {
            BoardsNames.Add(BoardName);
           
        }



        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (String s in e.NewItems)
                {
                    Controller.AddBoard(Email, s);
                }

            }
        }


    }
}
