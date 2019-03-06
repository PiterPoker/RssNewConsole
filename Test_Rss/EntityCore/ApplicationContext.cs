using Microsoft.EntityFrameworkCore;

namespace Test_Rss.EntityCore
{
    public class ApplicationContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<TuningGenerator> TuningGenerators { get; set; }
        public DbSet<Generator> Generators { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Generator>().HasData(
        //        new Generator[]
        //        {
        //        new Generator { Id=1, Name="www.interfax.by"},
        //        new Generator { Id=2, Name="habrahabr.ru"}
        //        });
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<TuningGenerator>().HasData(
        //        new TuningGenerator[]
        //        {
        //        new TuningGenerator { Id=1, UriString="https://www.interfax.by/news/feed",FormatTime="ddd, d MMM yyyy HH:mm:ss +0300",GeneratorId = 1},
        //        new TuningGenerator { Id=2, UriString="http://habrahabr.ru/rss/",FormatTime="ddd, d MMM yyyy HH:mm:ss GMT",GeneratorId = 2}
        //        });
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<News>().HasIndex(u => new { u.Title, u.DatePublication });
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer("Data Source = den1.mssql7.gear.host; Integrated Security = False; User ID = *******; Password = ********; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }
    }
}
