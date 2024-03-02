using Newtonsoft.Json;
using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ECCHackaton24.Foundation.ImageFinder.Models;

namespace ECCHackaton24.Foundation.ServiceManager.Repository
{
    public class BaseRepository
    {
        protected ISearchIndex Index;

        public BaseRepository(string indexName)
        {
            Index = ContentSearchManager.GetIndex(indexName);
        }

        public ISearchIndex GetIndex()
        {
            return Index;
        }

        public static async Task<ApiResponse> GetImageLabels(string imageUrl)
        {
            // URL del API que deseas llamar
            string apiUrl = "https://vision.googleapis.com/v1/images:annotate?key=AIzaSyA_Uphvled0XrKtqKtRhy2u6iXRcUkGcdc";

            // Token de autenticación Bearer
            string accessToken = "ya29.c.c0AY_VpZivrtWzEWfW9l5yHF6dq_yQpSldMyXJ4AxDmL-_cPS7IPu72gJsXjc8dRM3Kz6qy0on3nXC2Ylnls7AIpX59JK-y2SyBda3YLEf4RDbBUjxvkE2Vtp5sMd_pFK5fbXC1at9Wq5fX7xpJxBFGaWy5SbelQVvPu7H8on9vzmBdJjUqr1hoMcjsgKo_ReMAr6GWjmaPutK3AkAX2BCpU5vW60PiY1NRYuBYpqOcCelkKAi08m-vUEmmgJs-5kgWonf3J79eY0uepz6j1PKpa8s0-mEGJjbahbK_bZJyFude0k4_zPO9Oc9G5C2vx-hd_e-o7cMo-O-OzMjar0UFFfz2syvnfR2DeFl8CzwhzbApnO2Ld2h8wQWmsFEGdDAc5kP9ZEN400C2x2-JjUkrrbOaFWw_Yr_mmIbWrVBby6SjX71Rai8whaodZ046JtJbo4I9yu-qn_rvxSl4-cjVbFZ4j7ea3Bo4_BekOIj-qt0B6swZkwu8O-5qkvcs13mYldf0kfpduwRpIpXtguo_bqehk4bz72J98IWh8pvjeW53sa51V9c89Zcb-5QfVwnouOxXWQR2m4Mkf_qBX7qw8duzaWFUbVcZbiafqzru67fBZuqzpZSlV-h0Od7WZmwm80O1r6UwryZto5cIoZaIsz-OrzoX8f0VoasljceMYm6QuSb3xBzktQjYftdMRBv8OzXhc85wlUVgQJc6RYJOfyqblthUlXIkxl-nFqjJU7vFp6idiiuRrpyJWuhXQYtVoFhMU17kyizrWRaJrhMOFFoy790Suv9_611ufV-h_yMc0J3au8ZzO5RqM2vatBQFeIxeb-M512hIkd2zQj-Onb2m1WkcQFQ5U1olyxof-Qs03qWVIUzSZjYsz-5k8I0Vpa9apgX14SuSj1V9v7681B_M4n0S4w0Blz0JwWJXJ-lUap0q8ZklvnZ3np8oY0_QhQfQfsu7dkRSrQq8vwkI6-fpvlfo9v-x_9eO-sF7Fpv9MakmqxOreU";

            // Parámetros y datos que deseas enviar
            string keyParameter = "AIzaSyA_Uphvled0XrKtqKtRhy2u6iXRcUkGcdc";
            string jsonData = "{\"requests\": [{\"image\": {\"content\": \"" + imageUrl + "\"},\"features\":[{\"type\": \"LABEL_DETECTION\"}]}]}";

            try
            {
                // Llamar al método para realizar la llamada POST al API y obtener la respuesta
                return await CallApiWithBearerToken(apiUrl, accessToken, keyParameter, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return null; // O maneja el error de otra manera según tus necesidades
            }
        }

        public static async Task<ApiResponse> CallApiWithBearerToken(string apiUrl, string accessToken, string keyParameter, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                // Configurar la cabecera de autorización Bearer
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    // Construir la URL con el parámetro key
                    string urlWithKey = $"{apiUrl}";

                    // Configurar el contenido de la solicitud POST
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Realizar la llamada POST al API y obtener la respuesta
                    HttpResponseMessage response = await client.PostAsync(urlWithKey, content);

                    // Verificar si la solicitud fue exitosa (código 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y devolver la respuesta del API
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                    }
                    else
                    {
                        // Manejar el error y devolver una cadena vacía o algún indicador de error
                        Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al llamar al API: " + ex.Message);
                    return null;
                }
            }
        }
    }
}