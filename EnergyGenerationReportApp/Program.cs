using EnergyReportGenerationApp.Infrastructure;
using EnergyReportGenerationApp.Interfaces;
using EnergyReportGenerationApp.Service;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using Topshelf;

namespace EnergyReportGenarationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();

                // Register Services 
                var serviceProvider = EnergyReportServiceBuilder.GetServiceProvider(services);

                //Read log4net config
                XmlConfigurator.Configure();

                // Topshelf hostfactory configuration
                HostFactory.Run(configurator =>
                {
                    configurator.SetServiceName("EnergyReportGenerationService");
                    configurator.SetDisplayName("EnergyReportGenerationService");
                    configurator.SetDescription("Windows Service to create Enenergy Generation Report ");
                    configurator.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(10)));

                    configurator.Service<EnergyReportGenerationService>(serviceConfigurator =>
                    {
                        var reportGenerationProcessor = serviceProvider.GetRequiredService<IReportGenerationProcessor>();

                        serviceConfigurator.ConstructUsing(() => new EnergyReportGenerationService(reportGenerationProcessor));

                        serviceConfigurator.WhenStarted((energyReportGenerationService, hostControl) => energyReportGenerationService.Start(hostControl));
                        serviceConfigurator.WhenStopped((energyReportGenerationService, hostControl) => energyReportGenerationService.Stop(hostControl));
                    });

                    configurator.StartAutomatically();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
