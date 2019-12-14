using ImportProductDAL.Models;
using ImportProductDAL.Services.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ImportProductDAL.Services.Implementation
{
    public class MongoDbService : IDbService
    {
        private readonly ILogger<MongoDbService> _logger;

        public MongoDbService(ILogger<MongoDbService> logger)
        {
            _logger = logger;
        }

        public void InsertProducts(IEnumerable<DbProduct> dbProductList)
        {
            _logger.LogInformation($"Insert Products in Mongo Db");

            //Procedure call to insert in DB.
        }
    }
}
