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
    /// Interaction logic for AddBoardWindow.xaml
    /// </summary>
    public partial class AddBoardWindow : Window
    {
        

        private AddBoardViewModel viewModel;
        public AddBoardWindow(UserModel user)
        {
            InitializeComponent();
            this.viewModel = new AddBoardViewModel(user);
            this.DataContext = viewModel;
        }

        private void Add_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddBoard();
            this.Close();
        }
    }
}
