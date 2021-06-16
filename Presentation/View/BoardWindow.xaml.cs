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

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            UserView userView = new UserView(user);
            userView.Show();
            this.Close();
        }

        private void New_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow newTaskWindow = new NewTaskWindow(user, Board);
            newTaskWindow.Show();
        }

        private void New_Column_Button_Click(object sender, RoutedEventArgs e)
        {
            NewColumnWindow newColumnWindow = new NewColumnWindow(user, Board);
            newColumnWindow.Show();
        }

        private void Remove_Column_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveColumn();
        }

        private void Edit_Column_Button_Click(object sender, RoutedEventArgs e)
        {
            ColumnModel column = viewModel.SelectedColumn;
            EditColumnWindow editColumnWindow = new EditColumnWindow(user, Board, column);
            editColumnWindow.Show();
        }

        private void Advance_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AdvanceTask();
        }

        private void Move_Left_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MoveColumnLeft();
        }

        private void Move_Right_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MoveColumnRight();
        }

        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            this.Hide();
            MainWindow mainView = new MainWindow();
            mainView.Show();
            this.Close();
        }

        private void Edit_Task_DueDate_Button_Click(object sender, RoutedEventArgs e)
        {
            TaskModel task = viewModel.SelectedTask;
            ChangeTaskDueDateWindow changeTaskDueDateWindow = new ChangeTaskDueDateWindow(user, Board, task);
            changeTaskDueDateWindow.Show();

        }

        private void Assign_Task_Button_Click(object sender, RoutedEventArgs e)
        {
            TaskModel task = viewModel.SelectedTask;
            AssignTaskWindow assignTaskWindow = new AssignTaskWindow(user, Board, task);
            assignTaskWindow.Show();
        }

        private void Sort_By_DueDate_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SortTasksByDueDate();
        }



        //private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    var t = (ListBox)sender;
        //    viewModel.SelectedItem((TaskModel)t.SelectedItem);
        //}
    }
}
