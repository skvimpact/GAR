using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GarPullerClient;
public class PullerClient
{
        private readonly string url;

        public PullerClient(string url)
        {
            this.url = url;
        }   
		public async Task<bool> UpdateList()
		{
			bool result = false;

			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await httpClient.GetAsync($"GarFile/UpdateList");

				if (response.IsSuccessStatusCode)
				{
					result = true;
				}
			}

            return result;
        }
		public async Task<bool> DownloadFile()
		{
			bool result = false;

			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await httpClient.GetAsync($"GarFile/Download");
				if (response.IsSuccessStatusCode)
				{
					result = true;
				}
			}

            return result;
        }		
		public async Task<bool> ProcessFile()
		{
			bool result = false;

			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await httpClient.GetAsync($"GarFile/Process");

				if (response.IsSuccessStatusCode)
				{
					result = true;
				}
			}

            return result;
        }			 
		public async Task<bool> PutDownloadedFile(string filePath, Guid correlationId)
		{
			bool result = false;
            var jsonString = JsonConvert.SerializeObject(new {filePath = filePath});

			
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");            

			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				httpClient.DefaultRequestHeaders.Add("X-correlationID", correlationId.ToString());
				HttpResponseMessage response = await httpClient.PostAsync("GarFile/PutDownloadFIASFile", httpContent);

				if (response.IsSuccessStatusCode)
				{
					result = true;
				}
			}

            return result;
        }
}        