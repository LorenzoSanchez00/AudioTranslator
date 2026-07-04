using AudioTranslator.Services;
using Microsoft.AspNetCore.Mvc;

namespace AudioTranslator.Controllers
{
    public class VozController : Controller
    {
        private readonly ElevenLabsService _elevenLabs;
        private readonly GoogleTranslateService _translate;

        public VozController(ElevenLabsService elevenLabs, GoogleTranslateService translate)
        {
            _elevenLabs = elevenLabs;
            _translate = translate;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Procesar(IFormFile audio, string idiomaDestino)
        {
            if (audio == null || audio.Length == 0)
                return BadRequest("No se recibió audio.");

            using var ms = new MemoryStream();
            await audio.CopyToAsync(ms);
            var audioBytes = ms.ToArray();

            // 1. Audio -> texto
            var textoOriginal = await _elevenLabs.TranscribirAsync(audioBytes, audio.FileName);

            // 2. Traducir texto
            var textoTraducido = await _translate.TraducirAsync(textoOriginal, idiomaDestino);

            // 3. Texto traducido -> audio
            var audioFinal = await _elevenLabs.GenerarVozAsync(textoTraducido, idiomaDestino);

            return File(audioFinal, "audio/mpeg", "traduccion.mp3");
        }
    }
}
