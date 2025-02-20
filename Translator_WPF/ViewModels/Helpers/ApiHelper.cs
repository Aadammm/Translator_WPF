using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Translator_WPF.ViewModels.Helpers
{
    public class ApiHelper
    {
        public static async Task<List<Languages>> GetLanguages()
        {
            string languagesUrl = "https://libretranslate.com/languages";
            using HttpClient client = new HttpClient();
            var result = await client.GetAsync(languagesUrl);
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
            string urlTranslate = "https://libretranslate.com/translate";
            using HttpClient client = new HttpClient();
            var requestData = new
            {
                q = TextToTranslate,
                source = fromLanguageCode,
                target = toLanguageCode,
                format = "text"
            };

            string jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(urlTranslate, content);

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
    public class Languages
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("targets")]
        public List<string> Targets { get; set; }
    }
}
