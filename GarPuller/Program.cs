using DataLayer.EfCode;
using FlowControl;
using GarPublicClient;
using GarPuller;
using GarPuller.Queue;
using GarPuller.ServiceLayer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Host.UseSerilog();


// builder.Host.u
builder.Host.UseWindowsService();
builder.Services.AddControllers();

builder.Services.AddDbContext<FlowContext>(opts => {
            opts.UseSqlServer(
                builder?.Configuration["ConnectionStrings:FlowConnection"] ?? throw new ArgumentException("FlowConnection"));
		});  
builder.Services.AddDbContext<DeltaContext>(opts => {
            opts.UseSqlServer(
                builder?.Configuration["ConnectionStrings:GarConnection"] ?? throw new ArgumentException("GarConnection"));
		});

builder.Services.AddDbContext<GarContext>(opts => {
            opts.UseSqlServer(
                builder?.Configuration["ConnectionStrings:GarConnection"] ?? throw new ArgumentException("GarConnection"));
		});  
builder.Services.AddScoped<FlowDbAccess>();
builder.Services.AddScoped<GarFileService>();       
builder.Services.AddScoped<DownloadService>();
builder.Services.AddSingleton(new PublicClient(
    new PublicClientConfiguration(){
    Url = builder?.Configuration["Gar:ProxyServer"] ?? throw new ArgumentNullException("ProxyServer")}));  
builder.Services.AddSingleton(new DirectoryService(builder?.Configuration["Folder"] ?? throw new ArgumentException("Folder")));  
builder.Services.AddHostedService<GarPullerControlService>();

builder.Services.AddSingleton<GarPullerDownloadedQueue>();
builder.Services.AddSingleton<GarPullerProcessQueue>();
builder.Services.AddSingleton<GarPullerGoQueue>();


      
var app = builder.Build();

// Initialize Serilog's logger
Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder?.Configuration)
  .CreateLogger();

app.MapControllers();

try{
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex.Message);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
