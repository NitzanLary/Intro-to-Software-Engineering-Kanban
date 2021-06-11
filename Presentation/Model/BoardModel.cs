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
        private readonly UserModel user;
        public ObservableCollection<ColumnModel> Columns { get; set; }
        
        
        public BoardModel(BackendController controller, UserModel user) : base(controller)
        {
            this.user = user;
            //Columns = new ObservableCollection<ColumnModel>(controller.getColumns(user.email)).
        }



    }
}
