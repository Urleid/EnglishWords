using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Urleid.EnglishWords
{
    /// <summary>
    /// Логика взаимодействия для MainFrameWindow.xaml
    /// </summary>
    public partial class MainFrameWindow : Page
    {
        Word targetWord;

        /// <summary>
        /// Срабатывает при открытии.
        /// </summary>
        public MainFrameWindow()
        {
            InitializeComponent();

            UpdateMainWord();
        }

        /// <summary>
        /// Смена главного слова.
        /// </summary>
        private void Click_Next_Button(object sender, RoutedEventArgs e)
        {
            if (mainWord.Text != "Словарь пуст")
            {
                targetWord = null;

                using (ApplicationContext db = new ApplicationContext())
                {
                    List<Word> words = db.Words.ToList();

                    UpdateMainWord();

                    knowButton.Visibility = Visibility.Visible;
                    dontKnowButton.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Вывод перевода.
        /// </summary>
        private void Click_DontKnow_Button(object sender, RoutedEventArgs e)
        {
            if (mainWord.Text != "Словарь пуст")
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    targetWord = db.Words.Where(b => b.Value == mainWord.Text).FirstOrDefault();

                    targetWord.SuccessCounter--;
                    db.SaveChanges();
                }

                mainWord.Text = $"{targetWord.Value} - {targetWord.Translation}";
                knowButton.Visibility = Visibility.Hidden;
                dontKnowButton.Visibility = Visibility.Hidden;
            }
        }

        private void Click_Know_Button(object sender, RoutedEventArgs e)
        {
            if (mainWord.Text != "Словарь пуст")
            {
                TranslationWindow translationWindow = new TranslationWindow();
                translationWindow.PreparationTranslation(mainWord.Text);
                knowButton.Visibility = Visibility.Hidden;
                dontKnowButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Обновление основного слова.
        /// </summary>
        private void UpdateMainWord()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                targetWord = null;
                Random rnd = new Random();
                List<Word> words = db.Words.ToList();

                if (words.Count == 0)
                {
                    mainWord.Text = "Словарь пуст";
                }
                else
                {
                    while (targetWord == null)
                    {
                        Word tempWord = words[rnd.Next(words.Count)];
                        Word tempWord2 = words[rnd.Next(words.Count)];
                        Word tempWord3 = words[rnd.Next(words.Count)];

                        tempWord = db.Words.Where(b => b.Value == tempWord.Value).FirstOrDefault();
                        tempWord2 = db.Words.Where(b => b.Value == tempWord2.Value).FirstOrDefault();
                        tempWord3 = db.Words.Where(b => b.Value == tempWord3.Value).FirstOrDefault();

                        if (tempWord.SuccessCounter <= tempWord2.SuccessCounter)
                        {
                            if (tempWord.SuccessCounter <= tempWord3.SuccessCounter)
                                targetWord = tempWord;
                            else
                                targetWord = tempWord3;
                        }
                        else
                        {
                            if (tempWord2.SuccessCounter <= tempWord3.SuccessCounter )
                                targetWord = tempWord2;
                            else
                                targetWord = tempWord3;
                        }
                    }

                    mainWord.Text = targetWord.Value;
                }
            }
        }
    }
}
