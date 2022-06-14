using InfoTrackWebScrapper.Models;

namespace InfoTrackWebScrapper.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ScrapperResult> ScrapperResults { get; set; }
    }
}
