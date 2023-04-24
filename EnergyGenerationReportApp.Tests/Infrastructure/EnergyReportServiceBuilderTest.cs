using EnergyReportGenerationApp.Handlers;
using EnergyReportGenerationApp.Infrastructure;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EnergyReportGenerationApp.Tests.Infrastructure
{
    [TestClass]
    public class EnergyReportServiceBuilderTest
    {
        private ServiceCollection _serviceCollection;

        [TestInitialize]
        public void InitTest()
        {
            this._serviceCollection = new ServiceCollection();
        }

        [TestMethod]
        public void EnergyReportServiceBuilderShouldHaveExpectedNumberofServicesInServiceCollection()
        {
            // Arrange & Act
            var result = EnergyReportServiceBuilder.GetServiceProvider(_serviceCollection);

            // Assert
            Assert.AreEqual(9, _serviceCollection.Count);
        }

        [TestMethod]
        public void EnergyReportServiceBuilderShouldAddServicesAsExpectedInServiceCollection()
        {
            // Arrange & Act
            var result = EnergyReportServiceBuilder.GetServiceProvider(_serviceCollection);

            // Assert
            Assert.IsTrue(ValidateService(typeof(IReportGenerationProcessor), typeof(ReportGenerationProcessor), ServiceLifetime.Scoped));
            Assert.IsTrue(ValidateService(typeof(IFileHandler), typeof(FileHandler), ServiceLifetime.Scoped));
            Assert.IsTrue(ValidateService(typeof(IXmlSerializationHandler), typeof(XmlSerializationHandler), ServiceLifetime.Scoped));
            Assert.IsTrue(ValidateService(typeof(IEnergyDataProcessingHandler), typeof(EnergyDataProcessingHandler), ServiceLifetime.Scoped));
            Assert.IsTrue(ValidateService(typeof(IActualHeatRateValueHandler), typeof(ActualHeatRateValueHandler), ServiceLifetime.Scoped));

        }

        private bool ValidateService(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            return this._serviceCollection.Any(service => service.ServiceType == serviceType
                                                && service.ImplementationType == implementationType
                                                && service.Lifetime == lifetime);
        }
    }
}
