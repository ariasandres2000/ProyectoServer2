using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Api.Services
{
    public class AIServices
    {
        private HttpClient client = new HttpClient();
        private string tokenAI = "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";
        private string URL = "https://api.openai.com";

        public async Task<string> Imagen(string instruccion, int cantidad, string tamano)
        {
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAI);

            var valor = new
            {
                prompt = instruccion,
                n = cantidad,
                size = tamano
            };

            var content = new StringContent(JsonConvert.SerializeObject(valor), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("/v1/images/generations", content);
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonRespuesta))
                    throw new Exception("Respuesta inesperada.");

                return jsonRespuesta;
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Editar(string entrada, string instruccion)
        {
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAI);

            var valor = new
            {
                model = "text-davinci-edit-001",
                input = entrada,
                instruction = instruccion
            };

            var content = new StringContent(JsonConvert.SerializeObject(valor), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("/v1/images/edits", content);
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonRespuesta))
                    throw new Exception("Respuesta inesperada.");

                return jsonRespuesta;
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Terminacion(string entrada)
        {
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAI);

            var valor = new
            {
                model = "text-davinci-003",
                prompt = entrada,
                max_tokens = 7,
                temperature = 0
            };

            var content = new StringContent(JsonConvert.SerializeObject(valor), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("/v1/images/edits", content);
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonRespuesta))
                    throw new Exception("Respuesta inesperada.");

                return jsonRespuesta;
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
