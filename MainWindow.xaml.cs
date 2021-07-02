using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class MainWindow : Window
    {
        MainFrameWindow mainFrameWindow;
        ListWordsWindow listWordsWindow;

        /// <summary>
        /// Срабатывает при открытии.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            listWordsWindow = new ListWordsWindow();
            mainFrameWindow = new MainFrameWindow();
            mainFrame.Content = mainFrameWindow;
        }

        /// <summary>
        /// Открытие страницы словаря.
        /// </summary>
        private void Click_Words_Button(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = listWordsWindow;
            borderWordsButton.Visibility = Visibility.Visible;
            borderLearnButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Открытие страницы для заучивания.
        /// </summary>
        private void Click_Learn_Button(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = mainFrameWindow;
            borderLearnButton.Visibility = Visibility.Visible;
            borderWordsButton.Visibility = Visibility.Hidden;

            using (ApplicationContext db = new ApplicationContext())
            {
                Word targetWord = null;
                Random rnd = new Random();
                List<Word> words = db.Words.ToList();

                if (words.Count == 0)
                {
                    mainFrameWindow.mainWord.Text = "Словарь пуст";
                }
                else
                {
                    while (targetWord == null)
                    {
                        Word tempWord = words[rnd.Next(words.Count)];
                        targetWord = db.Words.Where(b => b.Value == tempWord.Value).FirstOrDefault();
                    }

                    mainFrameWindow.mainWord.Text = targetWord.Value;
                    mainFrameWindow.knowButton.Visibility = Visibility.Visible;
                    mainFrameWindow.dontKnowButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void mainFrame_ContentRendered(object sender, EventArgs e)
        {
            mainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}
