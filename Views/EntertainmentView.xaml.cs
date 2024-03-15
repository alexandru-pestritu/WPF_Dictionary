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

namespace WpfDictionary.Views
{
    /// <summary>
    /// Interaction logic for EntertainmentView.xaml
    /// </summary>
    public partial class EntertainmentView : Window
    {
        public EntertainmentView()
        {
            InitializeComponent();
            this.Closing += EntertainmentView_Closing;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWindow("WordSearchView");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWindow("AdminView");
        }

        private void EntertainmentView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
