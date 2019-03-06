using System;

namespace Test_Rss.Model
{
    public class NewsModel
    {
        public string Generator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public DateTime DatePublication { get; set; } = DateTime.MinValue;
    }
}
