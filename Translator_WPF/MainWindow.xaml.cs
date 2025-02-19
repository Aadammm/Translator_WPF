using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Translator_WPF.Models;

namespace Translator_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fromLanguage.ItemsSource = Translator_WPF.Models.Language.Languages;
        }

        private void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //prelozit
            }
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fromLanguage.SelectedItem is not null)
            {
              //posle language kod do api 
            }
        }
    }
}