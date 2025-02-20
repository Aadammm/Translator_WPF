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
using System.Windows.Documents;
using Translator_WPF.ViewModels.Helpers;

namespace Translator_WPF.ViewModel
{
    public class Translate : INotifyPropertyChanged
    {
        private ObservableCollection<Languages> languages = new();
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

        public Translate()
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
                ToLanguages = Languages.Where(x => x.Targets.Contains(selectedLanguage.Code)).ToList();
            }
        }

    }
}
