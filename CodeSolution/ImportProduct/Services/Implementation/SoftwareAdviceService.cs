using ImportProduct.Models;
using ImportProduct.Services.Interface;
using ImportProductDAL.Models;
using ImportProductDAL.Services.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportProduct.Services.Implementation
{
    public class SoftwareAdviceService : IProductService
    {
        private readonly ILogger<SoftwareAdviceService> _logger;
        private readonly AppSettings _config;
        private readonly IDbService _dbService;

        public SoftwareAdviceService(ILogger<SoftwareAdviceService> logger, IOptions<AppSettings> config, IDbService dbService)
        {
            _logger = logger;
            _config = config.Value;
            _dbService = dbService;
        }

        public void Run()
        {
            _logger.LogWarning($"We are now in the SoftwareAdvice service of: {_config.ConsoleTitle}");
            var softwareAdviceProducts = FetchProducts();


            var dbProducts = TransformToDbProducts(softwareAdviceProducts);
            _dbService.InsertProducts(dbProducts);

        }

        private IEnumerable<DbProduct> TransformToDbProducts(IEnumerable<AdviceProducts> softwareAdviceProducts)
        {
            return
                softwareAdviceProducts.ToList().Select(x => new DbProduct()
                {
                    Categories = x.categories,
                    Title = x.title,
                    Twitter = x.twitter
                });
        }

        private IEnumerable<AdviceProducts> FetchProducts()
        {
            using (var reader = File.OpenText(_config.ProductFilePath))
            {
                try
                {
                    var json = reader.ReadToEnd();
                    var softwareAdviceProducts = JsonConvert.DeserializeObject<SoftwareAdviceModel>(json);
                    return softwareAdviceProducts.products;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                    throw;
                }
            }
        }
    }
}
