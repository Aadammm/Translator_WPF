using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Translator_WPF.ViewModels;
using Translator_WPF.ViewModels.Helpers;

namespace Translator_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string textToTranslate;
        TranslateVM TranslateVM;
        public MainWindow()
        {
            InitializeComponent();
            TranslateVM = (TranslateVM)DataContext;
        }
        private void TextToTranslate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is TranslateVM viewModel)
            {
                RichTextBox rtb = sender as RichTextBox;
                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                viewModel.TextToTranslate = textRange.Text.Trim();
            }
        }

        private async void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string translatedText =await TranslateVM.TranslateText(textToTranslate);
                translatedTextBlock.Text = translatedText; // Nastaví preklad do TextBlocku
            }
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(TextToTranslate.Document.ContentStart, TextToTranslate.Document.ContentEnd);
            textToTranslate= textRange.Text.Trim();
        }
    }
}
