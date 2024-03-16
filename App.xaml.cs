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
            WindowManager.WordSearchView = new Views.WordSearchView();
            WindowManager.AdminView = new Views.AdminView();
            WindowManager.EntertainmentView = new Views.EntertainmentView();
            WindowManager.LoginView = new Views.LoginView();
            WindowManager.ShowWindow("WordSearchView");
        }

    }
}
