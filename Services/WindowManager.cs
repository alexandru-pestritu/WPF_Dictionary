using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDictionary.Views;

namespace WpfDictionary.Services
{
    public class WindowManager
    {
        public static WordSearchView WordSearchView { get; set; }
        public static AdminView AdminView { get; set; }

        public static EntertainmentView EntertainmentView { get; set; }

        public static void ShowWindow(string windowName)
        {
            HideAll();
            switch (windowName)
            {
                case "WordSearchView":
                    WordSearchView = new WordSearchView();
                    WordSearchView.Show();
                    break;
                case "AdminView":
                    AdminView = new AdminView();
                    AdminView.Show();
                    break;
                case "EntertainmentView":
                    EntertainmentView = new EntertainmentView();
                    EntertainmentView.Show();
                    break;
                default:
                    break;
            }
        }

        public static void HideAll()
        {
            WordSearchView?.Hide();
            AdminView?.Hide();
            EntertainmentView?.Hide();
        }
    }
}
