using FlowControl;
using GarPublicClient;
using GarPuller.ServiceLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GarPuller.Tests;

public class GarFileServiceTest
{
	private readonly IConfiguration config;
	private readonly FlowContext flowContext;
	private readonly FlowDbAccess access;
	private readonly PublicClient client;

	public GarFileServiceTest()
	{
		config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		var flowOptionsBuilder = new DbContextOptionsBuilder<FlowContext>();
		flowOptionsBuilder.UseSqlServer(config["ConnectionStrings:FlowConnection"] ?? string.Empty);
		flowContext = new FlowContext(flowOptionsBuilder.Options);
		access = new FlowDbAccess(flowContext);
		client = new PublicClient(config["Gar:ProxyServer"] ?? string.Empty);
	}

	[Fact]
	public void Test1()
	{
		//var service = new GarFileService(access, client);
		//service.UpdateList();
	}
	[Fact]
	public void Test2()
	{
		var json =
		JsonConvert.SerializeObject(new { filePath = "/d/d"});
	}
}