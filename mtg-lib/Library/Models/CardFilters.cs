namespace mtg_lib.Library.Models;
public class CardFilters
{
    public string? Name { get; set; }
    public List<string>? Colors { get; set; }
    public string? Rarity { get; set; }
}

public enum CardSortOption
{
    Name,
    ManaCost,
    Power
}

public enum SortDirection
{
    Ascending,
    Descending
}
