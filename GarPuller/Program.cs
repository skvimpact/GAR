using DataLayer.EfCode;
using FlowControl;
using GarPublicClient;
using GarPuller.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<FlowContext>(opts => {
            opts.UseSqlServer(
                builder.Configuration["ConnectionStrings:FlowConnection"]);
		});  
builder.Services.AddDbContext<DeltaContext>(opts => {
            opts.UseSqlServer(
                builder.Configuration["ConnectionStrings:GarConnection"]);
		});

builder.Services.AddDbContext<GarContext>(opts => {
            opts.UseSqlServer(
                builder.Configuration["ConnectionStrings:GarConnection"]);
		});  
builder.Services.AddScoped<FlowDbAccess>();
builder.Services.AddScoped<GarFileService>();       
builder.Services.AddSingleton(new PublicClient(builder.Configuration["Gar:ProxyServer"]));  
builder.Services.AddScoped<DownloadService>();
      
var app = builder.Build();

app.MapControllers();

app.Run();
