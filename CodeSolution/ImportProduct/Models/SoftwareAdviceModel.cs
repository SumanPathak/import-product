using System.Collections.Generic;

namespace ImportProduct.Models
{
    public class SoftwareAdviceModel
    {
        public IEnumerable<AdviceProducts> products { get; set; }
    }

    public class AdviceProducts
    {
        public IEnumerable<string> categories { get; set; }
        public string twitter { get; set; }
        public string title { get; set; }


    }
}
