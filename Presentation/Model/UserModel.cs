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
            foreach (String name in controller.GetBoardsNames(Email))
            {
                BoardsNames.Add(name);
            }
            //BoardsNames.CollectionChanged += HandleChange;

        }

        public IList<String> GetBoardsNames()
        {
            return Controller.GetBoardsNames(this.Email);
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (String y in e.OldItems)
                {

                    Controller.AddBoard(Email, y);
                }

            }
        }


    }
}
