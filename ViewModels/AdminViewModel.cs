using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WpfDictionary.Models;
using WpfDictionary.MVVM;
using WpfDictionary.Services;

namespace WpfDictionary.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private ObservableCollection<Word> _filteredWords;
        private Word _selectedWord;
        private string _searchText = "";    
        private string _wordTerm;
        private string _wordDescription;
        private string _wordImagePath;
        private Category _selectedCategory;
        private string _newCategoryName;

        public RelayCommand AddOrUpdateWordCommand { get; private set; }
        public RelayCommand DeleteWordCommand { get; private set; }
        public RelayCommand LoadImageCommand { get; private set; }

        public ObservableCollection<Word> FilteredWords
        {
            get => _filteredWords;
            set { _filteredWords = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories => DataService.Instance.Categories;

        public Word SelectedWord
        {
            get => _selectedWord;
            set
            {
                _selectedWord = value;
                OnPropertyChanged();
                if (value != null)
                {
                    WordTerm = value.Term;
                    WordDescription = value.Description;
                    WordImagePath = value.ImagePath;
                    SelectedCategory = Categories.FirstOrDefault(c => c.Name == value.Category.Name);
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterWords();
            }
        }

        public string WordTerm
        {
            get => _wordTerm;
            set { _wordTerm = value; OnPropertyChanged(); }
        }

        public string WordDescription
        {
            get => _wordDescription;
            set { _wordDescription = value; OnPropertyChanged(); }
        }

        public string WordImagePath
        {
            get => _wordImagePath;
            set { _wordImagePath = value; OnPropertyChanged(); }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }

        public string NewCategoryName
        {
            get => _newCategoryName;
            set { _newCategoryName = value; OnPropertyChanged();
            }
        }

        public AdminViewModel()
        {
            FilteredWords = new ObservableCollection<Word>(DataService.Instance.Words);
            AddOrUpdateWordCommand = new RelayCommand(_ => AddOrUpdateWord(), _ => CanAddOrUpdateWord());
            DeleteWordCommand = new RelayCommand(_ => DeleteWord(), _ => SelectedWord != null);
            LoadImageCommand = new RelayCommand(_ => LoadImage());
            FilterWords();
        }

        private void AddOrUpdateWord()
        {
            Category category = null;

            if (!string.IsNullOrWhiteSpace(NewCategoryName))
            {
                var existingCategory = DataService.Instance.Categories.FirstOrDefault(c => c.Name.ToLower() == NewCategoryName.ToLower());
                if (existingCategory == null)
                {
                    category = new Category { Name = NewCategoryName };
                    DataService.Instance.Categories.Add(category);
                }
                else
                {
                    category = existingCategory;
                }
            }
            else if (SelectedCategory != null)
            {
                category = SelectedCategory;
            }

            if (category == null)
            {
                return;
            }

            if (SelectedWord == null)
            {
                if (string.IsNullOrWhiteSpace(WordImagePath))
                {
                    WordImagePath = "pack://application:,,,/Resources/Images/Words/no-image-available.jpg";
                }

                var newWord = new Word
                    {
                        Term = WordTerm,
                        Description = WordDescription,
                        ImagePath = WordImagePath,
                        Category = category
                    };
                    DataService.Instance.Words.Add(newWord);
            }
            else
            {
                DataService.Instance.Words.Remove(SelectedWord);
                SelectedWord.Term = WordTerm;
                SelectedWord.Description = WordDescription;
                
                SelectedWord.ImagePath = WordImagePath;
                SelectedWord.Category = category;
                DataService.Instance.Words.Add(SelectedWord);
            }
            ClearFields();
            FilterWords();
        }

        private void DeleteWord()
        {
            if (SelectedWord != null)
            {
                DataService.Instance.Words.Remove(SelectedWord);
                ClearFields();
                FilterWords();
            }
        }

        private void ClearFields()
        {
            SelectedWord = null;
            WordTerm = string.Empty;
            WordDescription = string.Empty;
            WordImagePath = string.Empty;
            SelectedCategory = null;
            NewCategoryName = string.Empty;
        }

        private void FilterWords()
        {
            var lowerCaseSearchText = SearchText.ToLower();
            var filtered = DataService.Instance.Words.Where(w =>
                w.Term.ToLower().StartsWith(lowerCaseSearchText)).ToList();
            filtered.Sort((w1, w2) => w1.Term.CompareTo(w2.Term));
            FilteredWords = new ObservableCollection<Word>(filtered);
        }

        private void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "WordImages", Path.GetFileName(fileName));

                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                File.Copy(fileName, destinationPath, true);

                WordImagePath = destinationPath;
            }
        }
        private bool CanAddOrUpdateWord()
        {
            return !string.IsNullOrWhiteSpace(WordTerm) && !string.IsNullOrWhiteSpace(WordDescription) && (SelectedCategory != null || !string.IsNullOrWhiteSpace(NewCategoryName));
        }

    }
}
