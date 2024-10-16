using mtg_lib.Library.Models;
using System.ComponentModel.DataAnnotations;
using mtg.Models;

public class DeckViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public CardIndexViewModel? AvailableCards { get; set; }

    [MinLength(60)]
    [MaxLength(60)]
    public int[]? SelectedCardIds { get; set; }
}
