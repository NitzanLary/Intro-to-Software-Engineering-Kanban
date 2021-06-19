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
    /// Interaction logic for NewColumnWindow.xaml
    /// </summary>
    public partial class NewColumnWindow : Window
    {
        private NewColumnViewModel viewModel;
        public NewColumnWindow(UserModel user, BoardModel board)
        {
            InitializeComponent();
            this.viewModel = new NewColumnViewModel(user, board);
            this.DataContext = viewModel;
        }

        private void Add_Column_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddColumn();
            this.Close();
        }
    }
}
