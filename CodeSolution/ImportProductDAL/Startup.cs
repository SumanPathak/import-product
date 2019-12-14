using ImportProductDAL.Models;
using ImportProductDAL.Services.Implementation;
using ImportProductDAL.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ImportProductDAL
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            //Register Service
            switch (GetDbProvider(configuration))
            {
                case DBServiceProvider.SqlDb:
                    serviceCollection.AddTransient<IDbService, SqlDbService>();
                    break;
                case DBServiceProvider.MongoDb:
                    serviceCollection.AddTransient<IDbService, MongoDbService>();
                    break;
                default:
                    throw new InvalidOperationException("Not a valid Sql Provider");
            }
        }

        private static DBServiceProvider GetDbProvider(IConfigurationRoot configuration)
        {
            return (DBServiceProvider)Enum.Parse(typeof(DBServiceProvider), configuration.GetSection("Configuration:DbServiceProvider").Value);
        }
    }
}
