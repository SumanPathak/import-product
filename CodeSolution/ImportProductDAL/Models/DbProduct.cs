using System.Collections.Generic;

namespace ImportProductDAL.Models
{
    public class DbProduct
    {
        public IEnumerable<string> Categories { get; set; }
        public string Twitter { get; set; }
        public string Title { get; set; }
    }
}