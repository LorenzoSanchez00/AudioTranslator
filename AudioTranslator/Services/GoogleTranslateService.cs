using GTranslate.Translators;
using System.Text.Json;

namespace AudioTranslator.Services
{
    public class GoogleTranslateService
    {
        private readonly GoogleTranslator _translator;

        public GoogleTranslateService()
        {
            _translator = new GoogleTranslator();
        }

        public async Task<string> TraducirAsync(string texto, string idiomaDestino)
        {
            var resultado = await _translator.TranslateAsync(texto, idiomaDestino);
            return resultado.Translation;
        }
    }
}
