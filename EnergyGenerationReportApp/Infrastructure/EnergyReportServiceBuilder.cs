using System;
using System.IO;
using EnergyReportGenerationApp.Handlers;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EnergyReportGenerationApp.Infrastructure
{
    /// <summary>
    /// EnergyReportServiceBuilder class for registering serviceprovider 
    /// </summary>
    public static class EnergyReportServiceBuilder
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            //Read configuration values from App settings
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true).Build();

            services.AddSingleton(configuration);
            services.AddScoped<IReportGenerationProcessor, ReportGenerationProcessor>();
            services.AddScoped<IEnenrgyGenerationReportHandler, EnenrgyGenerationReportHandler>();
            services.AddScoped<IFileHandler, FileHandler>();
            services.AddScoped<IXmlSerializationHandler, XmlSerializationHandler>();
            services.AddScoped<IEnergyDataProcessingHandler, EnergyDataProcessingHandler>();
            services.AddScoped<ITotalGenerationValueHandler, TotalGenerationValueHandler>();
            services.AddScoped<IActualHeatRateValueHandler, ActualHeatRateValueHandler>();
            services.AddScoped<IEmissionValueHandler, EmissionValueHandler>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
