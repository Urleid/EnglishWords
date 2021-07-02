using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Urleid.EnglishWords
{
    /// <summary>
    /// Логика взаимодействия для TranslationWindow.xaml
    /// </summary>
    public partial class TranslationWindow : Window
    {
        MainFrameWindow mainFrameWindow;
        public TranslationWindow()
        {
            InitializeComponent();
            mainFrameWindow = new MainFrameWindow();
        }

        public void PreparationTranslation(string mainWord)
        {
            Show();
            mainWordText.Text = mainWord;
        }

        private void Click_Accept_Button(object sender, RoutedEventArgs e)
        {
            Word userEnter = null;
            Word trueWord = null;
            Random rnd = new Random();

            using (ApplicationContext db = new ApplicationContext())
            {
                List<Word> words = db.Words.ToList();

                userEnter = db.Words.Where(b => b.Translation == textBoxTranslation.Text).FirstOrDefault();

                trueWord = db.Words.Where(b => b.Value == mainWordText.Text).FirstOrDefault();

                if (textBoxTranslation.Text == "" || textBoxTranslation.Text == "Правильно" || textBoxTranslation.Text == "Неправильно")
                {
                    Hide();
                }

                if (userEnter == trueWord)
                {
                    trueWord.SuccessCounter++;
                    db.SaveChanges();
                    textBoxTranslation.Text = "Правильно";
                    mainWordBackground.Background = Brushes.Green;
                }
                else
                {
                    textBoxTranslation.Text = "Неправильно";
                    mainWordBackground.Background = Brushes.Red;
                }
            }
        }

        private void MouseEnter_Clear(object sender, MouseEventArgs e)
        {
            if (textBoxTranslation.Text == "Неправильно")
            {
                textBoxTranslation.Text = "";
                mainWordBackground.Background = Brushes.White;
            }
            else if (textBoxTranslation.Text == "Правильно")
            {
                Hide();
            }
        }
    }
}
