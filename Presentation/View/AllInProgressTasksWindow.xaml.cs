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
    /// Interaction logic for AllInProgressTasksWindow.xaml
    /// </summary>
    public partial class AllInProgressTasksWindow : Window
    {
        UserModel user;

        private AllInProgressTasksViewModel viewModel;
        public AllInProgressTasksWindow(UserModel user)
        {
            InitializeComponent();
            this.viewModel = new AllInProgressTasksViewModel(user);
            this.DataContext = viewModel;
            this.user = user;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            UserView userView = new UserView(user);
            userView.Show();
            this.Close();
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            this.Hide();
            MainWindow mainView = new MainWindow();
            mainView.Show();
            this.Close();
        }

        private void Sort_By_DueDate_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SortTasksByDueDate();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilterTasks();
        }

        private void Edit_Task_DueDate_Button_Click(object sender, RoutedEventArgs e)
        {
            TaskModel task = viewModel.SelectedTask;
            ChangeTaskDueDateWindow changeTaskDueDateWindow = new ChangeTaskDueDateWindow(user, task);
            changeTaskDueDateWindow.Show();

        }
    }
}
