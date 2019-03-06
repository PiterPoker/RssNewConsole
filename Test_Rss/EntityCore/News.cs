using System;

namespace Test_Rss.EntityCore
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public DateTime DatePublication { get; set; }

        public int GeneratorId { get; set; } 
        public Generator Generator { get; set; }  
    }
}
