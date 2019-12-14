using System;
using System.IO;
using ImportProduct.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ImportProductDAL;
using ImportProduct.Services.Interface;
using ImportProduct.Services.Implementation;

namespace ImportProduct
{
    class Program
    {
        static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // run app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", false)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));
            ConfigureConsole(configuration);

            // add services

            switch (GetServiceProvider())
            {
                case ProductServiceProvider.capterra:
                    serviceCollection.AddTransient<IProductService, CapterraService>();
                    break;
                case ProductServiceProvider.softwareadvice:
                    serviceCollection.AddTransient<IProductService, SoftwareAdviceService>();
                    break;
                default:
                    throw new InvalidOperationException("Please provide valid source");
            }

            //Registed DAl Services
            Startup.ConfigureServices(serviceCollection, configuration);

            // add app
            serviceCollection.AddTransient<App>();
        }

        private static void ConfigureConsole(IConfigurationRoot configuration)
        {
            System.Console.Title = configuration.GetSection("Configuration:ConsoleTitle").Value;
        }

        private static ProductServiceProvider GetServiceProvider()
        {
            return (ProductServiceProvider)Enum.Parse(typeof(ProductServiceProvider), Environment.GetCommandLineArgs()[1].ToLower());
        }
    }
}
