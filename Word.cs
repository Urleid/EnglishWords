using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urleid.EnglishWords
{
    class Word
    {
        public int id { get; set; } 

        // Слово на английском.
        private string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }


        // Перевод слова.
        private string translation;
        public string Translation
        {
            get { return translation; }
            set { translation = value; }
        }

        // Счетчик успешных переводов.
        private int successCounter;
        public int SuccessCounter
        {
            get { return successCounter; }
            set { successCounter = value; }
        }

        public Word() { }
        public Word(string value, string translation, int successCounter = 0)
        {
            this.value = value;
            this.translation = translation;
            this.successCounter = successCounter;
        }
    }
}
