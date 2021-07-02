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
using System.Data.Entity;

namespace Urleid.EnglishWords
{
    public partial class ListWordsWindow : Page
    {
        private static bool stateOfOpenWindows;

        /// <summary>
        /// Срабатывает при открытии.
        /// </summary>
        public ListWordsWindow()
        {
            InitializeComponent();

            Update();
        }

        /// <summary>
        /// Добавление слова в словарь.
        /// </summary>
        private void Click_Add_Button(object sender, RoutedEventArgs e)
        {
            AddWordWindow addWordWindow = new AddWordWindow();

            if (stateOfOpenWindows == false)
            {
                StateOfOpenWindowsOpen();
                addWordWindow.Show();
            }
        }

        public void StateOfOpenWindowsClose()
        {
            stateOfOpenWindows = false;
            Update();
        }
        public void StateOfOpenWindowsOpen()
        {
            stateOfOpenWindows = true;
        }

        /// <summary>
        /// Обновление listWords.
        /// </summary>
        private void Click_Update_Button(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Word> words = db.Words.ToList();

                // Вывод слов в listWords.
                listWords.ItemsSource = words;
            }
        }
        /// <summary>
        /// Обновление listWords.
        /// </summary>
        public void Update()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Word> words = db.Words.ToList();

                // Вывод слов в listWords.
                listWords.ItemsSource = words;
            }
        }

        /// <summary>
        /// Удаление слова из словаря.
        /// </summary>
        private void Click_Remove_Button(object sender, RoutedEventArgs e)
        {
            Word removeWord = null;
            using(ApplicationContext db = new ApplicationContext())
            {
                // Поиск слова в базе данных по Value.
                removeWord = db.Words.Where(b => b.Value == textBlockTarget.Text.Trim()).FirstOrDefault();

                try
                {
                    db.Words.Remove(removeWord);
                    textBlockTarget.Text = "";
                }
                catch (ArgumentNullException)
                {
                    textBlockTarget.Text = "Данное слово не найдено";
                    textBlockTarget.Background = Brushes.Red;
                }
                db.SaveChanges();

                Update();
            }
        }

        /// <summary>
        /// Выбор слова из словаря по нажатию на него.
        /// </summary>
        private void MouseUp_Chooce_TextBlock(object sender, MouseButtonEventArgs e)
        {
            UIElementCollection uIE = (sender as WrapPanel).Children;
            textBlockTarget.Text = (uIE[0] as TextBlock).Text;
            textBlockTarget.Background = Brushes.White;
        }

        private void Click_Edit_Button(object sender, RoutedEventArgs e)
        {
            Word tryWord = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                // Поиск слова в базе данных по Value.
                tryWord = db.Words.Where(b => b.Value == textBlockTarget.Text.Trim()).FirstOrDefault();

                if (tryWord != null)
                {
                    EditWordWindow translationWindow = new EditWordWindow();
                    translationWindow.PreparationEdit(textBlockTarget.Text);
                }
                else
                {
                    textBlockTarget.Text = "Данное слово не найдено";
                    textBlockTarget.Background = Brushes.Red;
                }
            }
        }

        private void MouseEnter_Clear(object sender, MouseEventArgs e)
        {
            if (textBlockTarget.Text == "Данное слово не найдено")
            {
                textBlockTarget.Text = "";
                textBlockTarget.Background = Brushes.White;
            }
        }
    }
}
