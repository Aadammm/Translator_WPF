using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Translator_WPF.ViewModel;
using Translator_WPF.ViewModels.Helpers;

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
        }

        private void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}