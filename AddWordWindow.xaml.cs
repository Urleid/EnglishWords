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
using System.Windows.Shapes;
using System.Data.Entity;
using System.Threading;
using System.ComponentModel;

namespace Urleid.EnglishWords
{
    /// <summary>
    /// Логика взаимодействия для AddWordWindow.xaml
    /// </summary>
    public partial class AddWordWindow : Window
    {
        ListWordsWindow listWordsWindow;

        public AddWordWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Подтверждение изменений.
        /// </summary>
        private void Click_Accept_Button(object sender, RoutedEventArgs e)
        {
            bool error = false;

            if (textBoxValue.Text.Length >= 255)
            {
                textBoxValue.Text = "Максимальная длина слова превышена";
                textBoxValue.Background = Brushes.Red;
            }
            else if (textBoxValue.Text == "")
            {
                textBoxValue.Text = "Введите слово";
                textBoxValue.Background = Brushes.Red;
            }
            else if (textBoxTranslation.Text.Length >= 255)
            {
                textBoxTranslation.Text = "Максимальная длина перевода превышена";
                textBoxTranslation.Background = Brushes.Red;
            }
            else if (textBoxTranslation.Text == "")
            {
                textBoxTranslation.Text = "Введите перевод слова";
                textBoxTranslation.Background = Brushes.Red;
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        Word word = new Word(textBoxValue.Text.Trim(), textBoxTranslation.Text.Trim());
                        db.Words.Add(word);
                        db.SaveChanges();

                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        textBoxValue.Text = "Данное слово уже добавлено";
                        textBoxValue.Background = Brushes.Red;
                        textBoxTranslation.Background = Brushes.Red;
                        error = true;
                    }
                }

                if (error == false)
                {
                    listWordsWindow = new ListWordsWindow();
                    listWordsWindow.StateOfOpenWindowsClose();
                    Hide();
                }
            }
        }

        /// <summary>
        /// Срабатывает при закрытии окна
        /// </summary>
        private void AddWordWindow_Close(object sender, CancelEventArgs e)
        {
            listWordsWindow = new ListWordsWindow();
            listWordsWindow.StateOfOpenWindowsClose();
        }

        private void MouseEnter_ClearAll(object sender, MouseEventArgs e)
        {
            if (textBoxValue.Text == "Данное слово уже добавлено" || 
                textBoxValue.Text == "Введите слово" ||
                textBoxTranslation.Text == "Введите перевод слова")
            {
                textBoxValue.Background = Brushes.White;
                textBoxTranslation.Background = Brushes.White;
                textBoxValue.Text = "";
                textBoxTranslation.Text = "";
            }
        }

        private void MouseEnter_ClearValue(object sender, MouseEventArgs e)
        {
            if (textBoxValue.Text == "Данное слово уже добавлено")
            {
                textBoxValue.Background = Brushes.White;
                textBoxTranslation.Background = Brushes.White;
                textBoxValue.Text = "";
                textBoxTranslation.Text = "";
            }
            else if (textBoxValue.Text == "Введите слово" || textBoxValue.Text == "Максимальная длина слова превышена")
            {
                textBoxValue.Background = Brushes.White;
                textBoxValue.Text = "";
            }
        }

        private void MouseEnter_ClearTranslation(object sender, MouseEventArgs e)
        {
            if (textBoxTranslation.Text == "Введите перевод слова" || textBoxTranslation.Text == "Максимальная длина перевода превышена")
            {
                textBoxTranslation.Background = Brushes.White;
                textBoxTranslation.Text = "";
            }
        }
    }
}
