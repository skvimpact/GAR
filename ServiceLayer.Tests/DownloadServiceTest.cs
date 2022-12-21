using Microsoft.Extensions.Configuration;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using DataLayer.EfClasses;
using ServiceLayer.Infrastructure;
using ServiceLayer;
using FlowControl;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System;

namespace ServiceLayer.Tests;

public class DownloadServiceTest
{
    private readonly IConfiguration config;
    private readonly FlowContext flowContext;
    public DownloadServiceTest()
    {
        config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var flowOptionsBuilder = new DbContextOptionsBuilder<FlowContext>();
		flowOptionsBuilder.UseSqlServer(config["ConnectionStrings:FlowConnection"] ?? string.Empty);        
        flowContext = new FlowContext(flowOptionsBuilder.Options);    
    }
    [Fact]
    public void DownloadFiasFile()
    {
        //MyDownloadFile(
        //    //new Uri("https://fias-file.nalog.ru/downloads/2022.11.22/gar_delta_xml.zip"),
		//	new Uri("https://fias-file.nalog.ru/downloads/2022.11.08/gar_delta_xml.zip"),
		//	"/Users/skvimpact/src/AR/GAR/08-delta-gar.zip");

		//MyDownloadFile(
		//	//new Uri("https://fias-file.nalog.ru/downloads/2022.11.22/gar_delta_xml.zip"),
		//	new Uri("https://fias-file.nalog.ru/downloads/2022.11.18/gar_xml.zip"), // 2315419
		//	"/Users/skvimpact/src/AR/GAR/18-full-gar.zip");
	}

    [Fact]
    public void Test1()
    {
        var deltaOptionsBuilder = new DbContextOptionsBuilder<DeltaContext>();
		deltaOptionsBuilder.UseSqlServer(config["ConnectionStrings:GarConnection"] ?? string.Empty);
        DeltaContext deltaContext = new DeltaContext(deltaOptionsBuilder.Options);

        var garOptionsBuilder = new DbContextOptionsBuilder<GarContext>();
		garOptionsBuilder.UseSqlServer(config["ConnectionStrings:GarConnection"] ?? string.Empty);        
        GarContext garContext = new GarContext(garOptionsBuilder.Options);

        var logger = Mock.Of<ILogger<DownloadService>>();

        var ds = new DownloadService(logger, deltaContext, garContext);
        //ds.HandleZipFile("/Users/skvimpact/src/AR/GAR/2022.01.11_gar_delta_xml.zip");
        ds.HandleZipFile("/Users/skvimpact/data/GAR/2022.12.20_gar_delta_xml.zip");


        //ds.HandleZipFile("/Users/skvimpact/src/AR/GAR/18-full-gar.zip");
        //ds.HandleFile("AS_OBJECT_LEVELS_20221020_98f59579-1b85-4644-adef-0947ca540857.XML");
//        var g = ds.Count("OBJECT_LEVELS");
	}
    [Fact]
    public void FileInfo()
    {
        /*
        var client = new DownloadFileClient(config["Gar:DownloadFileServer"] ?? string.Empty);
        var c = client.GetLastDownloadFileInfo().Result;
        //var client2 = new GetAllDownloadFileInfoClient(config["Gar:GetAllDownloadFileInfo"] ?? string.Empty);
        var c2 = client.GetAllDownloadFileInfo().Result;        
        var t = 0;
    */
    }

    [Fact]
    public void Download()
    {
        /*
        var client = new DownloadFileClient(config["Gar:DownloadFileServer"] ?? string.Empty);
        client.DDD();
        */
    }
    [Fact]
    public async void FlowInit()
    {
        /*
        var client = new DownloadFileClient(config["Gar:DownloadFileServer"] ?? string.Empty);
        var allDownloadFileInfo = await client.GetAllDownloadFileInfo();
        var dbAccess = new FlowDbAccess(flowContext);
        foreach(var downloadFileInfo in allDownloadFileInfo)
        {
            DateTime dateResult;
            if (DateTime.TryParse(downloadFileInfo.Date, out dateResult))
            {
                var file = new GarFile
                {
                    Date = dateResult
                };
                dbAccess.Add(file);
            }
        }*/
    }   

    [Fact]
    public async void FlowUpdate()
    {
        /*
        var client = new DownloadFileClient(config["Gar:DownloadFileServer"] ?? string.Empty);
        var allDownloadFileInfo = await client.GetAllDownloadFileInfo();
        var dbAccess = new FlowDbAccess(flowContext);
        foreach(var downloadFileInfo in allDownloadFileInfo)
        {
            DateTime dateResult;
            if (DateTime.TryParse(downloadFileInfo.Date, out dateResult))
            {
                var file = new GarFile
                {
                    Date = dateResult,
                    Url = downloadFileInfo.GarXMLDeltaURL
                };
                dbAccess.Update(file);
            }
        }
        */
    }     

		public void MyDownloadFile(Uri url, string outputFilePath)
		{
			const int BUFFER_SIZE = 16 * 1024;
			using (var outputFileStream = File.Create(outputFilePath, BUFFER_SIZE))
			{
				var req = WebRequest.Create(url);
				using (var response = req.GetResponse())
				{
					using (var responseStream = response.GetResponseStream())
					{
						var buffer = new byte[BUFFER_SIZE];
						int bytesRead,totalBytesRead = 0;
                        int i = 0;
						do
						{
                            i++;
							bytesRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
                            totalBytesRead += bytesRead;
						
						    outputFileStream.Write(buffer, 0, bytesRead);
						    if ((i % 100) == 0)
                                Console.WriteLine(String.Format("Downloaded {0:n0} bytes", totalBytesRead));
						} while (bytesRead > 0);
					}
				}
			}
		}




        public async Task FF(string url, string outputFilePath)
        {
            const int BUFFER_SIZE = 16 * 1024;
            using (var outputFileStream = File.Create(outputFilePath, BUFFER_SIZE))
			{
                using (var httpClient = new HttpClient())
                {

                //httpClient.GetStreamAsync(url);

/*
					using (var responseStream = httpClient.GetStreamAsync(url))
					{
						var buffer = new byte[BUFFER_SIZE];
						int bytesRead;
						do
						{
							//bytesRead = responseStream.Read(buffer, 0, BUFFER_SIZE);
							//outputFileStream.Write(buffer, 0, bytesRead);
						} while (bytesRead > 0);
					}*/

/*
				httpClient.BaseAddress = new Uri(url);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await httpClient.GetAsync("WebServices/Public/GetAllDownloadFileInfo");

				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
                    httpClient.GetStreamAsync
					//jsonDecoded = JsonConvert.DeserializeObject<ICollection<DownloadFileInfo>>(json);
				}*/
                }
			}            
        }

    
     
}