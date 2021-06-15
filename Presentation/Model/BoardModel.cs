using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class BoardModel : NotifiableModelObject
    {
        //private readonly UserModel user;
        //public ObservableCollection<ColumnModel> Columns { get; set; }

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

        //private readonly List<ColumnModel> columns; // backlogs , inProgress, done (generic updatable)
        //need to be observable probably
        public ObservableCollection<ColumnModel> Columns { get; set; }

        private string UserEmail; //storing this user here is an hack becuase static & singletone are not allowed.
        //NOT GOOD. SHOULDNT GET USER EMAIL AS PARAMETER!
        public BoardModel(BackendController controller, string boardName, string creator, ObservableCollection<ColumnModel> columns, string userEmail) : base(controller)
        {

            Name = boardName;
            Creator = creator;
            this.Columns = columns;

            this.UserEmail = userEmail;
            //Columns = new ObservableCollection<ColumnModel>(controller.getColumns(user.email)).
        }

        private void HandleChangeColumns(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ColumnModel c in e.NewItems)
                {
                    Controller.AddColumn(UserEmail, Creator, Name, c.ColumnOrdinal, c.Name);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ColumnModel c in e.OldItems)
                {
                    Controller.RemoveColumn(UserEmail, Creator, Name, c.ColumnOrdinal);
                }
            }

            Columns.CollectionChanged += HandleChangeColumns;

        }

        public void AddColumn(string user, string creator, string boardName, int columnOrdinal, string ColumnNamem, int maxTasks)
        {
            Controller.AddColumn(user, Creator, boardName, columnOrdinal, ColumnNamem);
            ColumnModel newColumn = new ColumnModel(Controller, ColumnNamem, new ObservableCollection<TaskModel>(), creator, boardName, columnOrdinal, maxTasks);
            Columns.Add(newColumn);
        }

        public void RemoveColumn(ColumnModel column)
        {
            Controller.RemoveColumn(UserEmail, Creator, Name, column.ColumnOrdinal);
            Columns.Remove(column);


        }



    }
}
