using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Translator.API.Models;
using Translator.API.Rules;

namespace Translator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public TranslationController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> TranslateText([FromBody] TranslationRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { error = "Invalid request. JSON data expected." });
            }

            if (string.IsNullOrEmpty(request.Text))
            {
                return BadRequest(new { error = "Invalid request. Key 'text' is missing." });
            }

            try
            {
                TranslateBR translate = new TranslateBR(_clientFactory);
                string translatedText = await translate.TranslateToFrench(request.Text);

                return Ok(new { translation = translatedText });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Translation error: {ex.Message}" });
            }
        }
    }
}
