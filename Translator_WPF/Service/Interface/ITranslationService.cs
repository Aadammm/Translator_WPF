using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator_WPF.Models;

namespace Translator_WPF.Service.Interface
{
   public interface ITranslationService
    {
        Task<IReadOnlyList<Language>> GetLanguagesAsync();
        Task<string> TranslateTextAsync(string text, string sourceLanguageCode, string targetLanguageCode);
    }
}
