using ImportProduct.Models;
using ImportProduct.Services.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ImportProduct
{
    public class App
    {
        private readonly IProductService _productService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        public App(IProductService productService, IOptions<AppSettings> config, ILogger<App> logger)
        {
            _productService = productService;
            _logger = logger;
            _config = config.Value;
        }

        public void Run()
        {
            _logger.LogInformation($"This is a console application for {_config.ConsoleTitle}");
            _productService.Run();
            System.Console.ReadKey();
        }
    }
}
