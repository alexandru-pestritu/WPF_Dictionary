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
using WpfDictionary.Services;
using WpfDictionary.ViewModels;

namespace WpfDictionary.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            this.DataContext = loginViewModel;
            this.Closing += LoginView_Closing;
        }

        private void LoginView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWindow("WordSearchView");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWindow("EntertainmentView");
        }
    }
}
