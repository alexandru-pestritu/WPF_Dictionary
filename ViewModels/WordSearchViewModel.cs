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
    public class WordSearchViewModel : ViewModelBase
    {
        private ObservableCollection<Word> _filteredWords;
        private Word _selectedWord;
        private string _searchText = "";
        private Category _selectedCategory;
        private Visibility _listViewVisibility = Visibility.Collapsed;
        private Visibility _wordDetailsVisibility = Visibility.Collapsed;

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
                if(value!=null)
                {
                    ListViewVisibility = Visibility.Collapsed;
                    WordDetailsVisibility = Visibility.Visible;
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
                ListViewVisibility = Visibility.Visible;
                WordDetailsVisibility = Visibility.Collapsed;
            }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                FilterWords();
            }
        }

        public Visibility ListViewVisibility
        {
            get => _listViewVisibility;
            set { _listViewVisibility = value; OnPropertyChanged(); }
        }

        public Visibility WordDetailsVisibility
        {
            get => _wordDetailsVisibility;
            set { _wordDetailsVisibility = value; OnPropertyChanged(); }
        }

        public WordSearchViewModel()
        {
            FilteredWords = new ObservableCollection<Word>(DataService.Instance.Words);
            FilterWords();
        }

        private void FilterWords()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                var lowerCaseSearchText = SearchText.ToLower();
                List<Word> filtered;

                if (SelectedCategory != null)
                {
                    filtered = DataService.Instance.Words.Where(w =>
                        w.Term.ToLower().StartsWith(lowerCaseSearchText) &&
                        w.Category.Name == SelectedCategory.Name).ToList();
                }
                else
                {
                    filtered = DataService.Instance.Words.Where(w =>
                        w.Term.ToLower().StartsWith(lowerCaseSearchText)).ToList();
                }

                FilteredWords = new ObservableCollection<Word>(filtered.Take(10));
            }
            else
            {
                FilteredWords.Clear();
            }
        }
    }
}
