using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Translator_WPF.Models;

namespace Translator_WPF.ViewModels.Helpers
{
    public class ApiHelper
    {
        private const string ApiKey = "xxxx";
        private const string getLanguagesUrl = "https://libretranslate.com/languages";
        private const string translateUrl = "https://libretranslate.com/translate";
        public static async Task<List<Languages>> GetLanguages()
        {           
            using HttpClient client = new HttpClient();
            var result = await client.GetAsync(getLanguagesUrl);
            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                var languages = JsonConvert.DeserializeObject<List<Languages>>(json);
                return languages;
            }
            else
            {
                return [];
            }

        }

        public static async Task<string> TranslateText(string TextToTranslate, string fromLanguageCode,string toLanguageCode)
        {
            using HttpClient client = new HttpClient();
            var requestData = new
            {
                q = TextToTranslate,
                source = fromLanguageCode,
                target = toLanguageCode,
                format = "text",
                api_key = ApiKey
            };

            string jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(translateUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonResponse);
               return json["translatedText"]?.ToString() ?? "Error: No translation";


            }
            else
            {
                return "Translation failed." + response.ReasonPhrase;
            }
        }
    }
}
