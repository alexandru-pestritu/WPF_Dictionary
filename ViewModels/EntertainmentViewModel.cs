using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDictionary.Models;
using WpfDictionary.MVVM;
using WpfDictionary.Services;



namespace WpfDictionary.ViewModels
{
    public class EntertainmentViewModel : ViewModelBase
    {
        private Random _random = new Random();
        private ObservableCollection<WordGameEntry> _selectedWords;
        private Word _currentWord;
        private int _currentWordIndex;
        private int _gameProgress;
        private int _score;
        private string _userInput = string.Empty;
        private Visibility _imageVisibilty = Visibility.Collapsed;
        private Visibility _descriptionVisibility = Visibility.Collapsed;
        private string _nextButtonText = "Înainte";
        private bool _isNextButtonEnabled = false;
        private bool _isCheckWordButtonEnabled = true;

        public RelayCommand NextWordCommand { get; private set; }
        public RelayCommand CheckWordCommand { get; private set; }

        public EntertainmentViewModel()
        {
            SelectedWords = new ObservableCollection<WordGameEntry>();
            NextWordCommand = new RelayCommand(_ => NextWord());
            CheckWordCommand = new RelayCommand(_ => CheckWord());
            ResetGame();
        }

        public ObservableCollection<WordGameEntry> SelectedWords
        {
            get => _selectedWords;
            set { _selectedWords = value; OnPropertyChanged(); }
        }

        public Word CurrentWord
        {
            get => _currentWord;
            set
            { _currentWord = value; OnPropertyChanged(); }
        }

        public int GameProgress
        {
            get => _gameProgress;
            set { _gameProgress = value; OnPropertyChanged(); }
        }

        public int Score
        {
            get => _score;
            set { _score = value; OnPropertyChanged(); }
        }

        public string UserInput
        {
            get => _userInput;
            set { _userInput = value; OnPropertyChanged(); }
        }

        public Visibility ImageVisibility
        {
            get => _imageVisibilty;
            set { _imageVisibilty = value; OnPropertyChanged(); }
        }

        public Visibility DescriptionVisibility
        {
            get => _descriptionVisibility;
            set { _descriptionVisibility = value; OnPropertyChanged(); }
        }

        public string NextButtonText
        {
            get => _nextButtonText;
            set { _nextButtonText = value; OnPropertyChanged(); }
        }

        public bool IsNextButtonEnabled
        {
            get => _isNextButtonEnabled;
            set { _isNextButtonEnabled = value; OnPropertyChanged(); }
        }

        public bool IsCheckWordButtonEnabled
        {
            get => _isCheckWordButtonEnabled;
            set { _isCheckWordButtonEnabled = value; OnPropertyChanged(); }
        }

        private void SelectWords()
        {
            var words = DataService.Instance.Words.OrderBy(w => _random.Next()).Take(5).ToList();
            _selectedWords = new ObservableCollection<WordGameEntry>(
                words.Select(w => new WordGameEntry
                {
                    Word = w,
                    UseDescription = w.ImagePath == "pack://application:,,,/Resources/Images/Words/no-image-available.jpg" || _random.Next(2) == 0
                }));

            OnPropertyChanged(nameof(SelectedWords));
        }

        private void ResetGame()
        {
            SelectWords();
            _currentWordIndex = 0;
            GameProgress = 0;
            Score = 0;
            UserInput = string.Empty;
            UpdateCurrentWordSettings();
            IsCheckWordButtonEnabled = true;
            IsNextButtonEnabled = false;
        }

        private void UpdateCurrentWordSettings()
        {
            var entry = SelectedWords[_currentWordIndex];
            CurrentWord = entry.Word;
            DescriptionVisibility = entry.UseDescription ? Visibility.Visible : Visibility.Collapsed;
            ImageVisibility = !entry.UseDescription ? Visibility.Visible : Visibility.Collapsed;

            NextButtonText = _currentWordIndex == SelectedWords.Count - 1 ? "Finalizează" : "Înainte";
        }

        private void NextWord()
        {
            if (_currentWordIndex + 1 < SelectedWords.Count)
            {
                _currentWordIndex++;
                GameProgress = _currentWordIndex;
                UpdateCurrentWordSettings();
                IsNextButtonEnabled = false;
                IsCheckWordButtonEnabled = true;
                UserInput = string.Empty;
            }
            else
                if(_nextButtonText == "Finalizează")
            {
                MessageBox.Show($"Jocul s-a terminat! Ai obținut {Score} puncte.", "Sfârșitul jocului", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
            }
                
        }

        private void CheckWord()
        {
            if(UserInput?.ToLower() == CurrentWord.Term.ToLower())
            {
                Score+=20;
                MessageBox.Show("Corect! Ai obținut 20 de puncte!", "Răspuns Corect", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Greșit! Răspunsul corect era: {CurrentWord.Term}", "Răspuns Greșit", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            IsNextButtonEnabled = true;
            IsCheckWordButtonEnabled = false;
        }
    }
}
