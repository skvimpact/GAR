using System;
using GarPullerClient;
namespace GarPullerClient.Tests;

public class GarFileControllerTest
{
    [Fact]
    public void Test1()
    {
        var client = new PullerClient("http://localhost:5125");
        var result = client.PutDownloadedFile("Through class lib", new Guid("c4580c54-9534-4951-856f-d0c16785fc90"));
    }
    [Fact]
    public async Task GarFileList()
    {
        var client = new PullerClient("http://localhost:5125");
        var result = await client.GarFilesList();
    }    
}