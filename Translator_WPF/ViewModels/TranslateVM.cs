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
using Translator_WPF.ViewModels.Helpers;

namespace Translator_WPF.ViewModels
{
    public class TranslateVM : INotifyPropertyChanged
    {
        private ObservableCollection<Languages> languages = [];
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

        //private string translateResult;

        //public string TranslateResult
        //{
        //    get { return translateResult; }
        //    set { translateResult = value; }
        //}

        public ObservableCollection<Languages> Languages
        {
            get => languages;
            set
            {
                languages = value;
                OnPropertyChanged();
            }
        }
        private Languages selectedToLanguage;
        public Languages SelectedToLanguage
        {
            get => selectedToLanguage;
            set
            {
                selectedToLanguage = value;
                OnPropertyChanged();
            }
        }
        private List<Languages> toLanguages;
        public List<Languages> ToLanguages
        {
            get => toLanguages;
            set
            {
                toLanguages = value;
                OnPropertyChanged();
            }
        }

        private Languages selectedLanguage;
        public Languages SelectedLanguage
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
            LoadLanguages();

        }

        public async void LoadLanguages()
        {
            var langs = await ApiHelper.GetLanguages();
            Languages = new ObservableCollection<Languages>(langs);
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
            return await ApiHelper.TranslateText(textToTranslate, selectedLanguage.Code, selectedToLanguage.Code);
        }
    }
}