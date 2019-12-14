using ImportProductDAL.Models;
using System.Collections.Generic;

namespace ImportProductDAL.Services.Interface
{
    public interface IDbService
    {
        void InsertProducts(IEnumerable<DbProduct> dbProductList);
    }
}
