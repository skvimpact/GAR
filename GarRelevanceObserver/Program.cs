using GarPullerClient;
using GarRelevanceObserver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            q.AddJobAndTrigger<Observer>(hostContext.Configuration);
            q.AddJobAndTrigger<Executor>(hostContext.Configuration);            
        });

        services.AddQuartzHostedService(
            q => q.WaitForJobsToComplete = true);

        services.AddSingleton(new PullerClient(hostContext.Configuration["Puller:Host"]));             
    })
    .Build();

await host.RunAsync();
