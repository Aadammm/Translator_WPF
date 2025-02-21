using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Translator_WPF.Models;
using Translator_WPF.Service.Interface;
using Translator_WPF.ViewModels.Helpers;

namespace Translator_WPF.ViewModels
{
    public class TranslateVM : INotifyPropertyChanged
    {
        private ObservableCollection<Language> languages = [];
        private readonly ITranslationService translationService;
        private string textToTranslate;
        public string TextToTranslate
        {
            get { return textToTranslate; }
            set
            {
                textToTranslate = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Language> Languages
        {
            get => languages;
            set
            {
                languages = value;
                OnPropertyChanged();
            }
        }
        private Language selectedToLanguage;
        public Language SelectedToLanguage
        {
            get => selectedToLanguage;
            set
            {
                selectedToLanguage = value;
                OnPropertyChanged();
            }
        }
        private List<Language> toLanguages;
        public List<Language> ToLanguages
        {
            get => toLanguages;
            set
            {
                toLanguages = value;
                OnPropertyChanged();
            }
        }

        private Language selectedLanguage;
        public Language SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                selectedLanguage = value;
                OnPropertyChanged();
                UpdateToLanguages();
            }
        }

        public TranslateVM()
        {
            translationService = new TranslationService(new HttpClient());
            LoadLanguages();
        }

        public async void LoadLanguages()
        {
            var langs = await translationService.GetLanguagesAsync();
            Languages = new ObservableCollection<Language>(langs);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void UpdateToLanguages()
        {
            if (SelectedLanguage != null)
            {
                ToLanguages = Languages.Where(x => selectedLanguage.Targets.Contains(x.Code)).ToList();
            }
        }
        private string GetTextFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text.Trim();
        }

        public async Task<string>  TranslateText(string textToTranslate)
        {
            return await translationService.TranslateTextAsync(textToTranslate, selectedLanguage.Code, selectedToLanguage.Code);
        }
    }
}