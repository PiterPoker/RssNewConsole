using System.Collections.Generic;

namespace Test_Rss.EntityCore
{
    public class Generator
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TuningGenerator TuningGenerator { get; set; }
        public List<News> ListNews { get; set; }

    }
}
