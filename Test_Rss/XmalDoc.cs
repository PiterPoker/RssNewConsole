using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Test_Rss.EntityCore;

namespace Test_Rss
{
    public class XmalDoc
    {
        private ApplicationContext db;
        private static int CountReadPost = 0;
        private static int CountSavePost = 0;

        public XmalDoc()
        {
            db = new ApplicationContext();
        }

        public void LoadXmalDoc(string responseBody, string generator, string maskTime, int idTuning, int idGenerator)
        {
            TuningGenerator tuning = db.TuningGenerators.Find(idTuning);
            XDocument xdoc = XDocument.Parse(responseBody.TrimStart('\n'));
            var items = from xe in xdoc.Element("rss").Element("channel").Elements("item")
                        where DateTime.ParseExact(xe.Element("pubDate").Value.ToString(), maskTime, CultureInfo.InvariantCulture) >= tuning.DateWrite
                        && xe.Element("title").Value.ToString() != tuning.TitleLastPost 
                        && xe.Element("guid").Value.ToString() != tuning.LinkLastPost
                        select new News
                        {
                            Title = Regex.Replace(xe.Element("title").Value.ToString(), @"(\<.*?\>)|(&\S*;)", ""),
                            Description = Regex.Replace(xe.Element("description").Value.ToString(), @"(\<.*?\>)|(&\S*;)", ""),
                            Link = xe.Element("guid").Value.ToString(),
                            Author = xe.Element("{http://purl.org/dc/elements/1.1/}creator").Value.ToString(),
                            DatePublication = DateTime.ParseExact(xe.Element("pubDate").Value.ToString(), maskTime, CultureInfo.InvariantCulture),
                            GeneratorId = idGenerator
                        };
            CountSavePost = items.ToList().Count();
            if (CountSavePost != 0)
                WriteNews(items.ToList(), tuning);
            
            CountReadPost = db.News.AsNoTracking().Where(p => p.GeneratorId == idGenerator).Count();
            Console.WriteLine("Новорсти портала {0}. Всего новостей прочитано {1}. Сохранено новостей последним обновлением {2}", db.Generators.Find(idGenerator).Name, CountReadPost, CountSavePost);
        }

        private void WriteNews(List<News> items, TuningGenerator tuning)
        {
            var sortedItems = from u in items
                              orderby u.DatePublication descending
                              select u;
            if (tuning != null && items.Count != 0)
            {
                tuning.DateWrite = sortedItems.FirstOrDefault().DatePublication;
                tuning.TitleLastPost = sortedItems.FirstOrDefault().Title;
                tuning.LinkLastPost = sortedItems.FirstOrDefault().Link;
                db.TuningGenerators.Update(tuning);
                db.News.AddRange(sortedItems.ToList());
                db.SaveChanges();
            }            
        }
    }
}
