using GarProxy;
using GarPublicClient;
using GarPullerClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

/*
builder.Services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });
*/ 

builder.Services.AddControllers();
builder.Services.AddSingleton(new PublicClient(builder.Configuration["Gar:PublicServer"]));   
builder.Services.AddSingleton(new PullerClient(builder.Configuration["Puller:Host"]));   
builder.Services.AddHostedService<DownloadService>();
builder.Services.AddSingleton<DownloadServiceQueue>();
var app = builder.Build();

app.MapControllers();

app.Run();
