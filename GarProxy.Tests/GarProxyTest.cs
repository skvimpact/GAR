using GarPublicClient;
using GarServices;

namespace GarProxy.Tests;

public class GarProxyTest
{
    private const string serverUrl = "http://localhost:5039";

    [Fact]
    public void GetLastDownloadFileInfoTest()
    {
        DownloadFileInfo? r1 = null;

        var client = new PublicClient(serverUrl);
        try
        {
            r1 = client.GetLastDownloadFileInfo().Result;
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

        var client = new PublicClient(serverUrl);
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
    public void GetDownloadFiasFileTest()
    {
        var client = new PublicClient(serverUrl);
        try
        {
            bool r1 = client.DownloadFiasFile(
                "https://fias-file.nalog.ru/downloads/2022.12.06/gar_delta_xml.zip",
                //  "https://fias-file.nalog.ru/downloads/2022.12.07/gar_delta_xml.zip",
                // "https://fias-file.nalog.ru/downloads/2022.01.04/gar_delta_xml.zip",
                Guid.NewGuid()).Result;
            Console.WriteLine("DownloadFiasFile >> " + r1);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}