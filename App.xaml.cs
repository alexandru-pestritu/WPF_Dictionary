using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfDictionary.Services;

namespace WpfDictionary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            WindowManager.WordSearchView = new Views.WordSearchView();
            WindowManager.AdminView = new Views.AdminView();
            WindowManager.EntertainmentView = new Views.EntertainmentView();
            WindowManager.ShowWindow("WordSearchView");
        }

    }
}
