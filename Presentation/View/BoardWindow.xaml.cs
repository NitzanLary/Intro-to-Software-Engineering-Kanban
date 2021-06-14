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

        //private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    var t = (ListBox)sender;
        //    viewModel.SelectedItem((TaskModel)t.SelectedItem);
        //}
    }
}
