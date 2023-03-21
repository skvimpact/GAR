using System.Net.Http.Headers;
using System.Text;
using GarServices;
using Newtonsoft.Json;
namespace GarPublicClient
{
	public class PublicClientConfiguration
	{
		public string Url { get; set; } = "https://fias.nalog.ru/WebServices/Public/";
		public string? CertificareName { get; set; }
		//public bool UseAttributes { get; set; } = false;
		//public bool UseCertificate { get; set; } = false;
	}
    public class PublicClient
    {
		private readonly PublicClientConfiguration _configuration = new PublicClientConfiguration();
		private readonly CertifiedHttpClient _httpClient;

        public PublicClient(PublicClientConfiguration? configuration = null)
        {
			if (configuration != null)
            	_configuration = configuration;
			_httpClient = new CertifiedHttpClient(_configuration.CertificareName)
			{
				 BaseAddress = new Uri(_configuration.Url) 
			};
        }	
        public async Task<DownloadFileInfo?> GetLastDownloadFileInfo()
        {	
			var request = new
				HttpRequestMessage(HttpMethod.Get, new Uri("getLastDownloadFileInfo", UriKind.Relative));
			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var jsonDecoded = JsonConvert.DeserializeObject<DownloadFileInfo>(
				await response.Content.ReadAsStringAsync());
			return jsonDecoded;
		}

		public async Task<IEnumerable<DownloadFileInfo>?> GetAllDownloadFileInfo()
		{
			var request = new
				HttpRequestMessage(HttpMethod.Get, new Uri("getAllDownloadFileInfo", UriKind.Relative));
			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var jsonDecoded = JsonConvert.DeserializeObject<ICollection<DownloadFileInfo>>(
				await response.Content.ReadAsStringAsync());							
            return jsonDecoded;
        }

		public async Task<bool> DownloadFiasFile(string downloadFileURL, Guid correlationId)
		{
			var jsonString = JsonConvert.SerializeObject(new {DownloadFileURL = downloadFileURL});		
			var request = new
				HttpRequestMessage(HttpMethod.Post, new Uri("downloadFiasFile", UriKind.Relative));	
				// request.Headers.Accept.Clear();
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Headers
				.Add("X-correlationID", correlationId.ToString().Replace("-", String.Empty));
			request.Content = new StringContent(jsonString, Encoding.UTF8);
			request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");		
			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			return true;	
        }   
    }
}
