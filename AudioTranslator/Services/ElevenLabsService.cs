using System.Net.Http.Headers;
using System.Text.Json;

namespace AudioTranslator.Services
{
    public class ElevenLabsService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly string _voiceId; // ID de la voz que quieras usar

        public ElevenLabsService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["ElevenLabs:ApiKey"]!;
            _voiceId = config["ElevenLabs:VoiceId"]!;
            _http.DefaultRequestHeaders.Add("xi-api-key", _apiKey);
        }

        // Speech-to-Text con el modelo Scribe
        public async Task<string> TranscribirAsync(byte[] audioBytes, string fileName)
        {
            using var content = new MultipartFormDataContent();
            var audioContent = new ByteArrayContent(audioBytes);
            audioContent.Headers.ContentType = new MediaTypeHeaderValue("audio/webm");
            content.Add(audioContent, "file", fileName);
            content.Add(new StringContent("scribe_v1"), "model_id");

            var response = await _http.PostAsync(
                "https://api.elevenlabs.io/v1/speech-to-text", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("text").GetString() ?? "";
        }

        // Text-to-Speech
        public async Task<byte[]> GenerarVozAsync(string texto, string idioma)
        {
            var body = new
            {
                text = texto,
                model_id = "eleven_multilingual_v2",
                voice_settings = new { stability = 0.5, similarity_boost = 0.75 }
            };

            var response = await _http.PostAsJsonAsync(
                $"https://api.elevenlabs.io/v1/text-to-speech/{_voiceId}", body);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
