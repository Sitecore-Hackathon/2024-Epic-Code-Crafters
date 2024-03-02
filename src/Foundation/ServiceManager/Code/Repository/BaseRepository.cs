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
            string accessToken = "ya29.c.c0AY_VpZhXkqdXFQ8pbgWee3WBl6cIvEQvxsWMfbzbyiWvMDHVcr-j4hmXzZ78h1HeiOZFRPAo4LumDLGs4yYO96i6INue1SuRenUXlVcqXCdtfG0jYBMk54rPnJYiF8wl-6MRPZcSUyXNwfFxaYTxtC9T4Ym8wGR5cLNSz600X9d1BICf-6SX8ggu9-raILQCMbW-5gAHVdZN-jk2M4ZFIGOodKNJwxVwzJ8stS0Br_YoVb4FuYNVZzYMVemgMXg0bklO99yze-njp9Ksg-8MtWDIq1VHjCh4iPJbWO8BIIrzXhJEISquYCeOpDfM4Rco8Zrb8l6O7VMV0hwbSqEK28lVwdke5Ne3ymiDs8-5fiMOQ0gANFdELfXc8-BFDQWubyyWL397D_hQjuWuOgOO4b18M23-zxkffMd-gWZUojoJnelR9opm0_Qy8hMObB2qOeVXJogv6-jzsxRv_7aO7y8Bt1ZVoiRMbdr4cp7yaW_tVSlI44dIZ0UaZ5XM9rxYWljaRrn72Bd8jdh3be5S_S3k9MysemO7u1V07d802QitXoI047bJo9Ryc_5WgcVRB0nZVsRvyh_tqhcY4gwO2cyBrkfOFjgI1cggIajI9xRb2SjB2BhxxIB_wc8s3cMl9gz3nRU1BUsxZnfr8_w-tgO5fc6q7tRdtgZZ__Yeg0WbBegk_JOZQhgx3dlFwgtc8cevgW3gugn1Okw-80xa3xJd1aWn-j4gOplmjXuzeeyewpF5aBrs_fcjvt239gIs5O29oIhWJi3FJ9ZOYdokBpm5xcW0wJeVai84tvS_IBbMeiQXYiBORRaJ7I2fF39OyZScyXp72OksRwZfOdOrvz1QvWv1_5tVmMB7I7g3cOafYtXkn6ZwJ8aw61zXOpbUgqXcQF5heoSy0y3cI-vVzmgMbhhwRn-xlMSmm74zRBrsySX30yJUjp7_j6h14baUsVWvBkziX_eopaUR9aV98yB1Upiaf1SgS_j3RZtUp5e7JRYrrUgf92a";

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