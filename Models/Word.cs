using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDictionary.Models
{
    public class Word
    {
        public string Term { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public string ImagePath { get; set; }    

        public Word() {}

        public Word(string term, string description, Category category, string imagePath = null)
        {
            Term = term;
            Description = description;
            Category = category;
            ImagePath = imagePath ?? "/Resources/Images/Words/no-image-available.jpg";
        }
    }
}
