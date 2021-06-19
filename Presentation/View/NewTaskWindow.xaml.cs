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
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        private NewTaskViewModel viewModel;

        public NewTaskWindow(UserModel user, BoardModel board)
        {
            InitializeComponent();
            this.viewModel = new NewTaskViewModel(user, board);
            this.DataContext = viewModel;
        }

        private void Add_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddTask();
            this.Close();
        }
    }
}
