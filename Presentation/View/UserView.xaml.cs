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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private UserViewModel viewModel;
        public UserView(UserModel user)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(user);
            this.viewModel = (UserViewModel)DataContext;
        }

        //Change function name and dependencies to Logout_Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            this.Hide();
            MainWindow mainView = new MainWindow();
            mainView.Show();
            this.Close();
        }
    }
}
