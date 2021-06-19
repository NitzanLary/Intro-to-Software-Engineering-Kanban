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
            this.viewModel = new UserViewModel(user);
            this.DataContext = viewModel;
            this.user = user;
        }

        //Change function name and dependencies to Logout_Click
        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            this.Hide();
            MainWindow mainView = new MainWindow();
            mainView.Show();
            this.Close();
        }

        UserModel user;
        private void Add_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            AddBoardWindow addBoardView = new AddBoardWindow(user);
            addBoardView.Show();
            //this.Close();
        }

        private void Remove_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveBoard();
        }

        private void Select_MyBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardModel board = viewModel.SelectMyBoard();
            BoardWindow boardView = new BoardWindow(user, board);
            boardView.Show();
            this.Close();
        }

        private void All_In_Progress_Tasks_Button_Click(object sender, RoutedEventArgs e)
        {
            AllInProgressTasksWindow allInProgressTasksView = new AllInProgressTasksWindow(user);
            allInProgressTasksView.Show();
            this.Close();
        }

        private void Join_Board_Button_Click(object sender, RoutedEventArgs e)
        {
            JoinBoardWindow joinBoardWindow = new JoinBoardWindow(user);
            joinBoardWindow.Show();

        }

        private void Select_AssignedBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardModel board = viewModel.SelectAssignedBoard();
            BoardWindow boardView = new BoardWindow(user, board);
            boardView.Show();
            this.Close();
        }
    }
}
