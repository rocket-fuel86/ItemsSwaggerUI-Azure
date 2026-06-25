using Newtonsoft.Json;
using System.Text;

namespace HW1.Services
{
    public class TranslateService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public TranslateService(IConfiguration config, HttpClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<string> TranslateTextAsync(string text, string toLang)
        {
            var key = _config["AzureTranslator:SubscriptionKey"];
            var endpoint = _config["AzureTranslator:Endpoint"];
            var region = _config["AzureTranslator:Region"];

            string route = $"/translate?api-version=3.0&&to={toLang}";
            var requestBody = new[] { new { Text = text } };
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endpoint + route),
                Content = content
            };

            request.Headers.Add("Ocp-Apim-Subscription-Key", key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", region);

            var response = await _client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseBody);
            return result[0].translations[0].text;
        }
    }
}
