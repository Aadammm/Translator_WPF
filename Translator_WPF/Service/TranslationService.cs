using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Translator_WPF.Models;
using Translator_WPF.Service.Interface;

namespace Translator_WPF.ViewModels.Helpers
{
    public class TranslationService : ITranslationService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "xxxx";
        private const string BaseUrl = "https://libretranslate.com";
        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }
        public async Task<IReadOnlyList<Language>> GetLanguagesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/languages");
                response.EnsureSuccessStatusCode();

                var languages = await response.Content.ReadFromJsonAsync<List<Language>>();
                return languages ?? [];
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        public async Task<string> TranslateTextAsync(string text, string sourceLanguageCode, string targetLanguageCode)
        {
            try
            {
                var request = new
                {
                    q = text,
                    source = sourceLanguageCode,
                    target = targetLanguageCode,
                    format = "text",
                    api_key = _apiKey
                };

                var response = await _httpClient.PostAsJsonAsync("/translate", request);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return result ?? "Error: No translation";
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                return $"Translation failed: {ex.Message}";
            }
        }
    }
}