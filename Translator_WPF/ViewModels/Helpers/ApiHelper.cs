using System.Text.Json.Serialization;

namespace Translator_WPF.ViewModels.Helpers
{
    public class ApiHelper
    {
        public static async Task GetLanguages()
        {

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
