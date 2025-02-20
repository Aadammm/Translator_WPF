using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Translator_WPF.ViewModels;

namespace Translator_WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TranslatorWindow : Window
    {
        private string textToTranslate;
        TranslateVM TranslateVM;
        public TranslatorWindow()
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
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(TextToTranslate.Document.ContentStart, TextToTranslate.Document.ContentEnd);
            textToTranslate = textRange.Text.Trim();
        }

        private async void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            string translatedText = await TranslateVM.TranslateText(textToTranslate);
            translatedTextBlock.Text = translatedText; 
        }
    }
}
