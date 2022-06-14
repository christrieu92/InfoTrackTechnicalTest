using InfoTrackWebScrapper.Models;

namespace InfoTrackWebScrapper.Repositories
{
    public class ScrapperRepository : IScrapperRepository
    {
        private readonly AppDbContext _dbContext;

        public ScrapperRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public async Task AddScrapperResultAsync(ScrapperResult scrapperResult)
        {
            await _dbContext.AddAsync(scrapperResult);
        }

        public async void Dispose()
        {
            await _dbContext.DisposeAsync();
        }

        public async Task<IEnumerable<ScrapperResult>> GetScrapperResultAsync()
        {
            return await _dbContext.ScrapperResults.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
