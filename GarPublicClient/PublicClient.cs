using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace GarPublicClient
{
    public class PublicClient
    {
        private readonly string url;

        public PublicClient(string url)
        {
            this.url = url;
        }

        public async Task<DownloadFileInfo?> GetLastDownloadFileInfo()
        {
            DownloadFileInfo? jsonDecoded = default(DownloadFileInfo);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("WebServices/Public/GetLastDownloadFileInfo");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
					jsonDecoded = JsonConvert.DeserializeObject<DownloadFileInfo>(json);
                }
            }

            return jsonDecoded;
        }

		public async Task<IEnumerable<DownloadFileInfo>?> GetAllDownloadFileInfo()
		{
			IEnumerable<DownloadFileInfo>? jsonDecoded = null;

			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await httpClient.GetAsync("WebServices/Public/GetAllDownloadFileInfo");

				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					jsonDecoded = JsonConvert.DeserializeObject<ICollection<DownloadFileInfo>>(json);
				}
			}

            return jsonDecoded;
        }

		public async Task<bool> DownloadFiasFile(string downloadFileURL, Guid correlationId)
		{
			bool result = false;
            var jsonString = JsonConvert.SerializeObject(new {DownloadFileURL = downloadFileURL});

			
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");            
			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				httpClient.DefaultRequestHeaders.Add("X-correlationID", correlationId.ToString());
				HttpResponseMessage response = await httpClient.PostAsync("WebServices/Public/DownloadFiasFile", httpContent);

				if (response.IsSuccessStatusCode)
				{
					result = true;
				}
			}
            return result;
        }
		public async Task<string>? Alive()
		{
			string jsonDecoded = "service isn't available";
			try
			{
				using (var httpClient = new HttpClient())
				{
					httpClient.BaseAddress = new Uri(url);
					httpClient.DefaultRequestHeaders.Accept.Clear();
					httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage response = await httpClient.GetAsync("WebServices/Public/Alive");

					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();
						jsonDecoded = json;
					}
				}
			}
			//catch(Exception ex)
			//{}
			finally{}
            return jsonDecoded;
        }     
    }
}
