using InfoTrackWebScrapper.Models;
using InfoTrackWebScrapper.Repositories;
using System.Text.RegularExpressions;

namespace InfoTrackWebScrapper.Services
{
    public class ScrapperService : IScrapperService
    {
        private readonly IScrapperRepository _scrapperRepository;

        public ScrapperService(IScrapperRepository scrapperRepository)
        {
            _scrapperRepository = scrapperRepository;
        }

        public async Task<ScrapperResult> Scrapper(string url, string keywords)
        {
            if (string.IsNullOrEmpty(keywords) || keywords.Length == 0)
            {
                throw new ArgumentNullException("Invalid keywords");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Invalid url");
            }

            var urlResponse = await GetUrlResponse(keywords);

            if (urlResponse == null)
            {
                throw new Exception("Response cannot be null");
            }

            var matchingLinks = FindMatchingLinks(urlResponse);

            var results = GetPositions(matchingLinks, url);

            var scrapperResult = new ScrapperResult
            {
                Url = url,
                Keywords = keywords,
                Ranking = results.Count == 0 ? "0" : String.Join(", ", results),
                Occurrences = results.Count(),
                SearchDate = DateTime.UtcNow.ToLocalTime()
            };

            await _scrapperRepository.AddScrapperResultAsync(scrapperResult);

            await _scrapperRepository.SaveAsync();

            return scrapperResult;
        }

        private async Task<string> GetUrlResponse(string keywords)
        {
            var urlSearch = "http://www.google.com/search?num=" +
                Constant.numberOfResults + $"&q=" + String.Join('+', keywords.Split(' '));

            HttpClient client = new HttpClient();

            string agent = "ClientDemo/1.0.0.1 test user agent DefaultRequestHeaders";

            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", agent);

            var response = await client.GetAsync(urlSearch);

            response.EnsureSuccessStatusCode();

            client.Dispose();

            return await response.Content.ReadAsStringAsync();
        }

        public List<string> GetPositions(MatchCollection mc, string url)
        {
            var results = new List<string>();

            var position = 0;

            foreach (Match m in mc)
            {
                if (m.Groups[0].Value.Contains(url))
                {
                    results.Add(position.ToString());
                }

                position++;
            }

            return results;
        }

        /// <summary> /// Examins  the search result and retrieves the position. /// </summary> 
        private MatchCollection FindMatchingLinks(string urlResponse)
        {
            Regex r = new Regex(@"(http|ftp|https):\/\/([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:\/~+#-]*[\w@?^=%&\/~+#-])");
            //Match m1 = r.Match(urlResponse);
            MatchCollection mc1 = r.Matches(urlResponse);

            return mc1;
        }

        public async Task<IEnumerable<ScrapperResult>> GetScrapperResult()
        {
            return await _scrapperRepository.GetScrapperResultAsync();
        }
    }
}
