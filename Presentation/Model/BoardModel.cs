using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly List<ColumnModel> columns; // backlogs , inProgress, done (generic updatable)
        public List<ColumnModel> Columns
        {
            get => columns;
        }

        public BoardModel(BackendController controller, string boardName, string creator, List<ColumnModel> columns) : base(controller)
        {

            Name = boardName;
            Creator = creator;
            this.columns = columns;

            //this.user = user;
            //Columns = new ObservableCollection<ColumnModel>(controller.getColumns(user.email)).
        }



    }
}
