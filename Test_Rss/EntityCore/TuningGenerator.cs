using System;

namespace Test_Rss.EntityCore
{
    public class TuningGenerator
    {
        public int Id { get; set; }
        public string UriString { get; set; }
        public string FormatTime { get; set; }
        public DateTime DateWrite { get; set; } = DateTime.MinValue;
        public string TitleLastPost { get; set; }
        public string LinkLastPost { get; set; }

        public int GeneratorId { get; set; } 
        public Generator Generator { get; set; }  
    }
}
