using Presentation.Model;
using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardViewModel viewModel;
        
        UserModel user;
        BoardModel Board; //maybe no need
        public BoardWindow(UserModel user, BoardModel selctedBoard)
        {
            InitializeComponent();
            this.viewModel = new BoardViewModel(user, selctedBoard);
            this.DataContext = viewModel;
            this.user = user;
            this.Board = selctedBoard; //maybe no need
        }
    }
}
