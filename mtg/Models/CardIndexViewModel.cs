using mtg_lib.Library.Models;
namespace mtg.Models
{
    public class CardIndexViewModel
    {
        public List<Card>? Cards { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PageViewModel? PageViewModel { get; internal set; }
        public string? Search { get; set; }
        public string? CardType { get; set; }
        public string? CardColor { get; set; }

    }
}
