namespace CatalogService.DTOs
{
    public class ContentItemDTO
    {
        public string Title { get; set; }

        public string PlotSummary { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Rating { get; set; }

        public string PosterURL { get; set; }

        public List<int> MainCastIds { get; set; }
    }
}
