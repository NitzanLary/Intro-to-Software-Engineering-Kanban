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
    /// Interaction logic for JoinBoardWindow.xaml
    /// </summary>
    public partial class JoinBoardWindow : Window
    {
        private JoinBoardViewModel viewModel;
        public JoinBoardWindow(UserModel user)
        {
            InitializeComponent();
            this.viewModel = new JoinBoardViewModel(user);
            this.DataContext = viewModel;
        }

        private void Join_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.joinBoard();
            this.Close();
        }
    }
}
