namespace InfoTrackWebScrapper.Dtos
{
    public class ScrapResultDto
    {
        public string Url { get; set; }

        public string Keywords { get; set; }

        public string Ranking { get; set; }

        public DateTime SearchDate { get; set; }

        public int Occurrences { get; set; }
    }
}
