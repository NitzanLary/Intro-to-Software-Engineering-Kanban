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
    /// Interaction logic for EditColumnWindow.xaml
    /// </summary>
    public partial class EditColumnWindow : Window
    {
        private EditColumnViewModel viewModel;
        public EditColumnWindow(UserModel user, BoardModel board)
        {
            InitializeComponent();
            this.viewModel = new EditColumnViewModel(user, board);
            this.DataContext = viewModel;
        }

        private void Edit_Column_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EditColumn();
            this.Close();
        }
    }
}
