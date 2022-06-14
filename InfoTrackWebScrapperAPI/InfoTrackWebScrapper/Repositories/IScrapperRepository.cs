using InfoTrackWebScrapper.Models;

namespace InfoTrackWebScrapper.Repositories
{
    public interface IScrapperRepository : IDisposable
    {
        Task<IEnumerable<ScrapperResult>> GetScrapperResultAsync();

        Task AddScrapperResultAsync(ScrapperResult scrapperResult);

        Task SaveAsync();
    }
}
