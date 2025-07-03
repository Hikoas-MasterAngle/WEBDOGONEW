using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WEBDOGO.Models
{
    public class sepay
    {
        private readonly string _url = "https://my.sepay.vn/userapi/transactions/list";
        private readonly string _token = "IMLAURGBZ16LO5ZFYUNQ6RLLOZN72VTSV4CKWSHW0FKPH3BETAW3JD1CHRBHFPJJ";

        public async Task<dynamic> FetchTransactionsAsync()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        return new { error = $"HTTP error! Status: {response.StatusCode}" };
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return data;
                }
                catch (Exception ex)
                {
                    return new { error = ex.Message };
                }
            }
        }
    }
}