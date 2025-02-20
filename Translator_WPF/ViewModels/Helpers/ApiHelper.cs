using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
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
