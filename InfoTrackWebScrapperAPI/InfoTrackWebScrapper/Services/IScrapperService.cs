using InfoTrackWebScrapper.Models;

namespace InfoTrackWebScrapper.Services
{
    public interface IScrapperService
    {
        Task<ScrapperResult> Scrapper(string url, string keywords);

        Task<IEnumerable<ScrapperResult>> GetScrapperResult();
    }
}
