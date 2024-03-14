using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WpfDictionary.Models;

namespace WpfDictionary.Services
{
    public class DataService
    {
        private const string WordsFilePath = "C:\\Users\\pestr\\source\\WpfDictionary\\Resources\\Data\\words.xml";
        private const string CategoriesFilePath = "C:\\Users\\pestr\\source\\WpfDictionary\\Resources\\Data\\categories.xml";

        public ObservableCollection<Word> Words { get; private set; }
        public ObservableCollection<Category> Categories { get; private set; }

        private static DataService _instance;
        public static DataService Instance => _instance ?? (_instance = new DataService());

        private DataService()
        {
            Words = DeserializeFromFile<ObservableCollection<Word>>(WordsFilePath) ?? new ObservableCollection<Word>();
            Categories = DeserializeFromFile<ObservableCollection<Category>>(CategoriesFilePath) ?? new ObservableCollection<Category>();

            Words.CollectionChanged += (s, e) => SerializeToFile(WordsFilePath, Words);
            Categories.CollectionChanged += (s, e) => SerializeToFile(CategoriesFilePath, Categories);
        }

        private void SerializeToFile<T>(string filePath, T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        private T DeserializeFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath)) return default(T);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
