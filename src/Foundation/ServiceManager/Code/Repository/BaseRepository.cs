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
using Sitecore.Configuration;

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

        /// <summary>
        /// Generate the image tags using Google AI
        /// </summary>
        /// <param name="imageUrl64"></param>
        /// <returns></returns>
        public static async Task<ApiResponse> GetImageLabels(string imageUrl64)
        {
            // API Url
            string apiUrl = Settings.GetSetting("AIApiUrl");

            // Token
            string accessToken = Settings.GetSetting("AIApiToken");            

            // Key
            string keyParameter = Settings.GetSetting("AIApiKey");             
            
            //body
            string jsonData = "{\"requests\": [{\"image\": {\"content\": \"" + imageUrl64 + "\"},\"features\":[{\"type\": \"LABEL_DETECTION\"}]}]}";

            try
            {                
                return await CallApiWithBearerToken(apiUrl, accessToken, keyParameter, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public static async Task<ApiResponse> CallApiWithBearerToken(string apiUrl, string accessToken, string keyParameter, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                // Authorization
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    // Parameter
                    string urlWithKey = $"{apiUrl}";

                    // Content
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Post request
                    HttpResponseMessage response = await client.PostAsync(urlWithKey, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                    }
                    else
                    {                        
                        Console.WriteLine("Error: " + response.StatusCode);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error API: " + ex.Message);
                    return null;
                }
            }
        }
    }
}