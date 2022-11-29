using Microsoft.EntityFrameworkCore;
using DataLayer.EfCode;

using Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadApp;
using GarPublicClient;
using FlowControl;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        //services.AddHostedService<Worker>();
        services.AddScoped<DownloadService>();
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            q.AddJobAndTrigger<DownloadJob>(hostContext.Configuration);
        });

        services.AddQuartzHostedService(
            q => q.WaitForJobsToComplete = true);
                    
		services.AddDbContext<DeltaContext>(opts => {
            opts.UseSqlServer(
                hostContext.Configuration["ConnectionStrings:GarConnection"]);//, options => options.CommandTimeout(300));                
		});

		services.AddDbContext<GarContext>(opts => {
            opts.UseSqlServer(
                hostContext.Configuration["ConnectionStrings:GarConnection"]);//, options => options.CommandTimeout(300));
		});  

		services.AddDbContext<FlowContext>(opts => {
            opts.UseSqlServer(
                hostContext.Configuration["ConnectionStrings:FlowConnection"]);
		});  

        services.AddSingleton(new PublicClient(hostContext.Configuration["Gar:GarConnection"]));     
    })
    .Build();

//await host.RunAsync();
host.Run();


/*
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DeltaContext>(opts => {
	opts.UseSqlServer(
		builder.Configuration["ConnectionStrings:GarConnection"]);
});

builder.Services.AddDbContext<GarContext>(opts => {
	opts.UseSqlServer(
		builder.Configuration["ConnectionStrings:GarConnection"]);
});

builder.Services.AddQuartz(q =>  
                {
					                        q.UseMicrosoftDependencyInjectionScopedJobFactory();

                        q.AddJobAndTrigger<DownloadJob>(builder.Configuration);				
                });

builder.Services.AddQuartzHostedService(
                    q => q.WaitForJobsToComplete = true);				

var app = builder.Build();

//var s = new DownloadService(EfCoreContext);
//app.MapGet("/", () => "Hello World!");ls


app.Run();
*/

/*
namespace DownloadApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();

                        q.AddJobAndTrigger<DownloadJob>(hostContext.Configuration);

                    });

                    services.AddQuartzHostedService(
                        q => q.WaitForJobsToComplete = true);
                    
					services.AddDbContext<DeltaContext>(opts => {
						opts.UseSqlServer(
						hostContext.Configuration["ConnectionStrings:GarConnection"]);
					});

					services.AddDbContext<GarContext>(opts => {
						opts.UseSqlServer(
						hostContext.Configuration["ConnectionStrings:GarConnection"]);
					});
                    
                });
    }
}

*/