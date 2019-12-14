using ImportProduct.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YamlDotNet.Serialization;
using System.Linq;
using ImportProductDAL.Models;
using ImportProductDAL.Services.Interface;
using ImportProduct.Services.Interface;

namespace ImportProduct.Services.Implementation
{
    public class CapterraService : IProductService
    {
        private readonly ILogger<CapterraService> _logger;
        private readonly AppSettings _config;
        private readonly IDbService _dbService;

        public CapterraService(ILogger<CapterraService> logger, IOptions<AppSettings> config, IDbService dbService)
        {
            _logger = logger;
            _config = config.Value;
            _dbService = dbService;
        }

        public void Run()
        {
            _logger.LogInformation($"We are now in the capterra service of: {_config.ConsoleTitle}");

            var capterraProducts = FetchProducts();

            var dbProducts = TransformToDbProducts(capterraProducts);
            _dbService.InsertProducts(dbProducts);
        }

        private IEnumerable<DbProduct> TransformToDbProducts(IEnumerable<CapterraModel> capterraProducts)
        {
            return
                capterraProducts.ToList().Select(x => new DbProduct()
                {
                    Categories = x.categories,
                    Title = x.name,
                    Twitter = x.twitter
                });
        }

        private IEnumerable<CapterraModel> FetchProducts()
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            using (var reader = File.OpenText(_config.ProductFilePath))
            {
                try
                {
                    var yaml = reader.ReadToEnd();

                    var deserialiser = new DeserializerBuilder().Build();
                    return deserialiser.Deserialize<IEnumerable<CapterraModel>>(yaml);
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
