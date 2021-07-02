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

namespace Urleid.EnglishWords
{
    /// <summary>
    /// Логика взаимодействия для EditWordWindow.xaml
    /// </summary>
    public partial class EditWordWindow : Window
    {
        public EditWordWindow()
        {
            InitializeComponent();
        }

        private void Click_Accept_Button(object sender, RoutedEventArgs e)
        {

            if (textBoxValue.Text.Length >= 255)
            {
                textBoxValue.Text = "Максимальная длина слова превышена";
                textBoxValue.Background = Brushes.Red;
            }
            else if (textBoxTranslation.Text.Length >= 255)
            {
                textBoxTranslation.Text = "Максимальная длина перевода превышена";
                textBoxTranslation.Background = Brushes.Red;
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Word editWord = null;
                    ListWordsWindow listWordsWindow = new ListWordsWindow();

                    // Поиск слова в базе данных по Value.
                    editWord = db.Words.Where(b => b.Value == textBlockTargetText.Text.Trim()).FirstOrDefault();

                    if (textBoxValue.Text != "")
                        editWord.Value = textBoxValue.Text;
                    if (textBoxTranslation.Text != "")
                        editWord.Translation = textBoxTranslation.Text;
                    if (clearSuccessCounter.IsChecked.Value)
                        editWord.SuccessCounter = 0;

                    db.SaveChanges();
                }
                    Hide();
            }
        }

        public void PreparationEdit(string textBlockTarget)
        {
            Show();
            textBlockTargetText.Text = textBlockTarget;
        }

        private void MouseEnter_ClearValue(object sender, MouseEventArgs e)
        {

            if (textBoxValue.Text == "Максимальная длина слова превышена")
            {
                textBoxValue.Background = Brushes.White;
                textBoxValue.Text = "";
            }
        }

        private void MouseEnter_ClearTranslation(object sender, MouseEventArgs e)
        {
            if (textBoxTranslation.Text == "Максимальная длина перевода превышена")
            {
                textBoxTranslation.Background = Brushes.White;
                textBoxTranslation.Text = "";
            }
        }
    }
}
