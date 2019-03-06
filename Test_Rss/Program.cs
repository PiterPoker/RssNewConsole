using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test_Rss.EntityCore;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace Test_Rss
{
    class Program
    {
        private static readonly HttpClient HttpClient;
        private static readonly XmalDoc xmalDoc;
        private static ApplicationContext db;
        private static Timer aTimer;

        static Program()
        {
            HttpClient = new HttpClient();
            xmalDoc = new XmalDoc();
            db = new ApplicationContext();
        }

        static async void RunMetoth(Object source, ElapsedEventArgs e)
        {        
            try
            {
                Console.Clear();
                foreach (var task in db.Generators.Include(u => u.TuningGenerator).ToList())
                {
                    await Task.Run(() => ReaderNews(task.TuningGenerator.UriString, task.Name, task.TuningGenerator.FormatTime, task.TuningGenerator.Id, task.Id));
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
            }
        }

        private static void SetTimer()
        {
            aTimer = new Timer(30000);
            aTimer.Elapsed += RunMetoth;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Чтение новостей начнется через 30 сек \n");
            SetTimer();
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
        }

        static async Task ReaderNews(string URI, string generator, string maskTime, int idTuning, int idGenerator)
        {
            try
            {
                var response = await HttpClient.GetAsync(URI);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                xmalDoc.LoadXmalDoc(responseBody, generator, maskTime, idTuning, idGenerator);
                //response.Dispose();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0}", e.Message);
            }
        }
    }
}

