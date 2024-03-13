using System;
using System.Collections.Generic;
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
        private const string WordsFilePath = "../Resources/Data/words.xml";
        private const string CategoriesFilePath = "../Resources/Data/categories.xml";
        private const string UsersFilePath = "../Resources/Data/users.xml";

        public List<Word> Words { get; private set; }
        public List<Category> Categories { get; private set; }
        public List<User> Users { get; private set; }

        private static DataService _instance;
        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataService();
                }
                return _instance;
            }
        }   

        private DataService()
        {
            Words = new List<Word>();
            Categories = new List<Category>();
            Users = new List<User>();
        }

        public void LoadData()
        {
            LoadWords();
            LoadCategories();
            LoadUsers();
        }

        private void LoadWords()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Word>));
            if (File.Exists(WordsFilePath))
            {
                using (StreamReader reader = new StreamReader(WordsFilePath))
                {
                    Words = (List<Word>)serializer.Deserialize(reader);
                }
            }
        }

        private void LoadCategories()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Category>));
            if (File.Exists(CategoriesFilePath))
            {
                using (StreamReader reader = new StreamReader(CategoriesFilePath))
                {
                    Categories = (List<Category>)serializer.Deserialize(reader);
                }
            }
        }

        private void LoadUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            if (File.Exists(UsersFilePath))
            {
                using (StreamReader reader = new StreamReader(UsersFilePath))
                {
                    Users = (List<User>)serializer.Deserialize(reader);
                }
            }
        }

        public void SaveData()
        {
            SaveWords();
            SaveCategories();
            SaveUsers();
        }

        private void SaveWords()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Word>));
            using (StreamWriter writer = new StreamWriter(WordsFilePath))
            {
                serializer.Serialize(writer, Words);
            }
        }

        private void SaveCategories()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Category>));
            using (StreamWriter writer = new StreamWriter(CategoriesFilePath))
            {
                serializer.Serialize(writer, Categories);
            }
        }

        private void SaveUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (StreamWriter writer = new StreamWriter(UsersFilePath))
            {
                serializer.Serialize(writer, Users);
            }
        }

    }
}
