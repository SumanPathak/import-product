using ImportProduct.Models;
using ImportProduct.Services.Implementation;
using ImportProduct.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Tests
{
    public class TestCapterraService
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            ImportProduct.Program.ConfigureServices(serviceCollection, new string[] { "capterra", "feed-products/capterra.yaml" });

            // create service provider
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void TestAppSettings()
        {
            var config = _serviceProvider.GetService<IOptions<AppSettings>>();

            Assert.AreEqual("capterra", config.Value.ProductProvider);
            Assert.AreEqual("feed-products/capterra.yaml", config.Value.ProductFilePath);
        }


        [Test]
        public void TestServiceProvider()
        {
            var productService = _serviceProvider.GetService<IProductService>();

            Assert.AreEqual(productService.GetType(), typeof(CapterraService));
        }
    }
}