using Newtonsoft.Json;

namespace Translator.API.Rules
{
    public class TranslateBR
    {
        private readonly IHttpClientFactory _clientFactory;
        public TranslateBR(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> TranslateToFrench(string text)
        {
            using var client = _clientFactory.CreateClient();
            string apiUrl = $"https://api.mymemory.translated.net/get?q={text}&langpair=en|fr";
            var response = await client.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
            string translatedText = jsonResponse.responseData.translatedText;

            return translatedText;
        }
    }
}
