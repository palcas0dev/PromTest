using Newtonsoft.Json; // Asegúrate de instalar Newtonsoft.Json
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace FRONT.Clases
{
    public class ApiCall
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> callApiJsonAsync(string apiUrl, string tipoHttp , string jsonBody, Dictionary<string, string> headers)
        {
            try {
                _httpClient.DefaultRequestHeaders.Clear();
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                if (headers == null) headers = new Dictionary<string, string>();
                foreach (var head in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(head.Key, head.Value);
                }

                switch (tipoHttp)
                {

                    case "POST":
                        var postRes = await _httpClient.PostAsync(apiUrl, content);
                        if (postRes.IsSuccessStatusCode)
                        {
                            var apiResponse = await postRes.Content.ReadAsStringAsync();
                            return apiResponse;
                        }
                        break;
                    case "PUT":
                        var putRes = await _httpClient.PutAsync(apiUrl, content);
                        if (putRes.IsSuccessStatusCode)
                        {
                            var apiResponse = await putRes.Content.ReadAsStringAsync();
                            return apiResponse;
                        }
                        break;
                    case "DELETE":
                        var delRes = await _httpClient.DeleteAsync(apiUrl);
                        if (delRes.IsSuccessStatusCode)
                        {
                            var apiResponse = await delRes.Content.ReadAsStringAsync();
                            return apiResponse;
                        }
                        break;
                    default: //GET
                        var getRes = await _httpClient.GetAsync(apiUrl);
                        if (getRes.IsSuccessStatusCode)
                        {
                            var apiResponse = await getRes.Content.ReadAsStringAsync();
                            return apiResponse;
                        }
                        break;

                }
            }
            catch {
            
            }

            return null;
        }
    }
}