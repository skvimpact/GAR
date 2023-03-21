using System.Diagnostics;
using GarPublicClient;
namespace GarPublicClient.Tests;

public class PublicClientTest
{
    [Fact]
    public void CheckCertifiedHttpClient()
    {
        var v = new CertifiedHttpClient();

    }
    [Fact]
    public void GetLastDownloadFileInfoTest()
    {
        DownloadFileInfo? r1 = null;

        var client = new PublicClient(new PublicClientConfiguration(){
            Url = "http://localhost:5039/WebServices/Public/"
        } );
        try
        {
            r1 = client?.GetLastDownloadFileInfo().Result;
            Console.WriteLine("GetLastDownloadFileInfoTest >> " + r1?.ToString());
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    [Fact]
    public void GetAllDownloadFileInfoTest()
    {
        IEnumerable<DownloadFileInfo>? r1 = null;

        var client = new PublicClient(new PublicClientConfiguration(){
            Url = "http://localhost:5039/WebServices/Public/"
        } );
        try
        {
            r1 = client.GetAllDownloadFileInfo().Result;
            foreach(var i in r1 ?? Enumerable.Empty<DownloadFileInfo>())
            Console.WriteLine("GetAllDownloadFileInfo >> " + i?.ToString());
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }  
    [Fact]
    public void DownloadTest()
    {
        bool r1 = false;

        var client = new PublicClient(new PublicClientConfiguration(){
            Url = "http://localhost:5039/WebServices/Public/"
        } );
        try
        {
            r1 = client.DownloadFiasFile("https://fias-file.nalog.ru/downloads/2023.02.17/gar_delta_xml.zip", Guid.NewGuid()).Result;
            Console.WriteLine($"DownloadFiasFile >> {r1}");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [Fact]
    public void GuidTest()
    {
        var gu = Guid.NewGuid();
        Guid gy;
        var yyy = Guid.NewGuid().ToString().Replace("-", String.Empty);
        if (Guid.TryParse(yyy, out var newGuid))
        {
            
            Console.WriteLine($"{newGuid}");
        }
        
        Console.WriteLine($"{gu}");
    }    
}