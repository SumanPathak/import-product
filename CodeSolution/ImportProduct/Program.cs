using ImportProduct.Models;
using ImportProduct.Services.Implementation;
using ImportProduct.Services.Interface;
using ImportProductDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ImportProduct
{
    public class Program
    {
        static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, args);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // run app
            serviceProvider.GetService<App>().Run();
        }

        public static void ConfigureServices(IServiceCollection serviceCollection, string[] args)
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
                .AddCommandLine(args)
                .Build();       

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"))
                .Configure<AppSettings>(x=>x.ProductProvider = GetServiceProvider(args).ToString())
                .Configure<AppSettings>(x=>x.ProductFilePath = GetFilePath(args));

            ConfigureConsole(configuration);

            // add services

            switch (GetServiceProvider(args))
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

        private static ProductServiceProvider GetServiceProvider(string[] args)
        {
            try
            {
                return (ProductServiceProvider)Enum.Parse(typeof(ProductServiceProvider), args[0].ToLower());
            }
            catch
            {
                return ProductServiceProvider.unknown;
            }
        }
        private static string GetFilePath(string[] args)
        {
            try
            {
                return args[1];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
