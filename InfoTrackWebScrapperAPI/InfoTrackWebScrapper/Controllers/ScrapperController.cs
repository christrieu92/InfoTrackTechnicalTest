using InfoTrackWebScrapper.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackWebScrapper.Controllers
{
    [EnableCors()]
    [ApiController]
    [Route("[controller]")]
    public class ScrapperController : ControllerBase
    {
        private readonly IScrapperService _scrapperService;

        private readonly ILogger<ScrapperController> _logger;

        public ScrapperController(IScrapperService scrapperService, ILogger<ScrapperController> logger)
        {
            _scrapperService = scrapperService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Scrap([FromBody] ScrapDto scrap)
        {
            try
            {
                _logger.LogInformation("Starting to begin scrapping");

                var result = await _scrapperService.Scrapper(scrap.Url, scrap.Keywords);

                if (result == null)
                {
                    _logger.LogWarning("Failed to Scrap information from browser");

                    return NotFound();
                }

                _logger.LogInformation("Successfully Scrapped information");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Error occured {ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetScrapHistory()
        {
            _logger.LogInformation("Starting to retrieve scrapping history");

            var history = await _scrapperService.GetScrapperResult();

            if (!history.Any())
            {
                return NotFound();
            }

            return Ok(history.Select(s => new ScrapResultDto
            {
                Url = s.Url,
                Keywords = s.Keywords,
                Ranking = s.Ranking,
                SearchDate = s.SearchDate,
                Occurrences = s.Occurrences
            }).ToList());
        }
    }
}