using System.Collections.Generic;

namespace ImportProduct.Models
{
    public class CapterraModel
    {
        public IEnumerable<string> categories { get { return tags.Split(','); } }
        public string tags { get; set; }
        public string name { get; set; }
        public string twitter { get; set; }
    }
}
