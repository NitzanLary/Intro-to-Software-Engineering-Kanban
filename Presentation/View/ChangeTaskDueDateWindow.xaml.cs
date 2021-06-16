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
    /// Interaction logic for ChangeTaskDueDateWindow.xaml
    /// </summary>
    public partial class ChangeTaskDueDateWindow : Window
    {
        private ChangeTaskDueDateViewModel viewModel;
        public ChangeTaskDueDateWindow(UserModel user, BoardModel board, TaskModel task)
        {
            InitializeComponent();
            this.viewModel = new ChangeTaskDueDateViewModel(user, board, task);
            this.DataContext = viewModel;
        }

        private void Edit_Task_DueDate_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EditTaskDueDate();
            this.Close();
        }
    }
}
