using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDictionary.MVVM;
using WpfDictionary.Services;

namespace WpfDictionary.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private bool _isLoginEnabled;
        
        public RelayCommand LoginCommand { get; private set; }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                IsLoginEnabled = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                IsLoginEnabled = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
        }

        public bool IsLoginEnabled
        {
            get => _isLoginEnabled;
            set { _isLoginEnabled = value; OnPropertyChanged(); }
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => Login(), _ => IsLoginEnabled);
        }

        private void Login()
        {
            bool isUserValid = AuthService.Login(Username, Password);
            if(isUserValid)
            {
                WindowManager.ShowWindow("AdminView");
                Username = null;
                Password = null;
            }
            else
            {
                MessageBox.Show("Username sau parola incorecte!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                Username = null;
                Password = null;
            }
        }
    }
}
