using System.Net.Http.Headers;
using System.Text;
using GarServices;
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
            using var client = new HttpClient(){ BaseAddress = new Uri(url) };
			var request = new
				HttpRequestMessage(HttpMethod.Get, new Uri("WebServices/Public/GetLastDownloadFileInfo", UriKind.Relative));
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var jsonDecoded = JsonConvert.DeserializeObject<DownloadFileInfo>(
				await response.Content.ReadAsStringAsync());
			return jsonDecoded;
		}

		public async Task<IEnumerable<DownloadFileInfo>?> GetAllDownloadFileInfo()
		{
            using var client = new HttpClient(){ BaseAddress = new Uri(url) };
			var request = new
				HttpRequestMessage(HttpMethod.Get, new Uri("WebServices/Public/GetAllDownloadFileInfo", UriKind.Relative));
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var jsonDecoded = JsonConvert.DeserializeObject<ICollection<DownloadFileInfo>>(
				await response.Content.ReadAsStringAsync());							
            return jsonDecoded;
        }

		public async Task<bool> DownloadFiasFile(string downloadFileURL, Guid correlationId)
		{
			var jsonString = JsonConvert.SerializeObject(new {DownloadFileURL = downloadFileURL});			
			var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");            
			using var httpClient = new HttpClient();					
			httpClient.BaseAddress = new Uri(url);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("X-correlationID", correlationId.ToString());
			HttpResponseMessage response = await httpClient.PostAsync("WebServices/Public/DownloadFiasFile", httpContent);
			response.EnsureSuccessStatusCode();
			return true;	
        }   
    }
}
