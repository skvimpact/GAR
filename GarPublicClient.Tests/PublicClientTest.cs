using System.Diagnostics;
using GarPublicClient;
namespace GarPublicClient.Tests;

public class PublicClientTest
{
    [Fact]
    public void GetLastDownloadFileInfoTest()
    {
        DownloadFileInfo? r1 = null;

        var client = new PublicClient("http://fias.nalog.ru");
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

        var client = new PublicClient("http://fias.nalog.ru");
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
}