using mtg_lib.Library.Models;

namespace mtg.Models
{
    public class DeckDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Card>? Cards { get; set; }
        public string? UserId { get; set; }
    }
}