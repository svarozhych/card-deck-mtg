using System;
using System.Collections.Generic;

namespace mtg_lib.Library.Models;

public partial class Deck
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int[] CardsId { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual AspNetUser? User { get; set; }
}
