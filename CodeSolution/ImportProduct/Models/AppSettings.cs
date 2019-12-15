using System;

namespace ImportProduct.Models
{
    public class AppSettings
    {
        /// <summary>
        /// Sets header title in console window
        /// </summary>
        public string ConsoleTitle { get; set; }
        public string DbServiceProvider { get; set; }
        public string ProductProvider { get; set; }
        public string ProductFilePath { get; set; }
    }

}
