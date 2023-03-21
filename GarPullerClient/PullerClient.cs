using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GarServices;
using FlowControl;
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
		public async Task<ServiceState> Go()
		{
				using var httpClient = new HttpClient();
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await httpClient.GetAsync($"GarFile/Go");
				response.EnsureSuccessStatusCode();	
				return (ServiceState)Int32.Parse(await response.Content.ReadAsStringAsync());
				//return (ServiceState)BitConverter.ToInt32(await response.Content.ReadAsByteArrayAsync(), 0);

        }			 
		public async Task<bool> PutDownloadedFile(string filePath, Guid correlationId)
		{			
			using var httpClient = new HttpClient();
        	var jsonString = JsonConvert.SerializeObject(new {filePath = filePath});			
        	var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");    				
			httpClient.BaseAddress = new Uri(url);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("X-correlationID", correlationId.ToString());
			HttpResponseMessage response = await httpClient.PostAsync("GarFile/PutDownloadFIASFile", httpContent);
			response.EnsureSuccessStatusCode();	
			return true;
        }
		public async Task<IEnumerable<GarFile>?> GarFilesList()
		{
            using var client = new HttpClient(){ BaseAddress = new Uri(url) };
			var request = new
				HttpRequestMessage(HttpMethod.Get, new Uri("GarFile/List", UriKind.Relative));
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var jsonDecoded = JsonConvert.DeserializeObject<ICollection<GarFile>>(
				await response.Content.ReadAsStringAsync());							
            return jsonDecoded;
        }		
}        