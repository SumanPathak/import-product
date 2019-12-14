using ImportProductDAL.Models;
using ImportProductDAL.Services.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ImportProductDAL.Services.Implementation
{
    public class SqlDbService : IDbService
    {
        private readonly ILogger<SqlDbService> _logger;

        public SqlDbService(ILogger<SqlDbService> logger)
        {
            _logger = logger;
        }

        public void InsertProducts(IEnumerable<DbProduct> dbProductList)
        {
            _logger.LogInformation($"Insert Products in Sql Db");

            //Procedure call to insert in DB.
        }
    }
}
