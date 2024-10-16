namespace mtg.Models
{
    public class CardDetails
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Set { get; set; } = null!;

        public string? ManaCost { get; set; }

        public string Type { get; set; } = null!;

        public string? Text { get; set; }

        public string Color { get; set; } = null!;

        public string? OriginalImageUrl { get; set; }

        public string? Rarity { get; set; }

        public string? Artist { get; set; }

        public string? Flavor { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }

        public string Number { get; set; } = null!;

        public string? Symbol { get; set; }

        public long nextId { get; set; }
        public long prevId { get; set; }

        public static List<string> ConvertManaCostToList(string manaCost)
        {
            List<string> symbols = new List<string>();

            if (!string.IsNullOrEmpty(manaCost))
            {
                string cleanedManaCost = manaCost.Replace("{", "").Replace("}", ",");

                cleanedManaCost = cleanedManaCost.Substring(0, cleanedManaCost.Length - 1);

                string[] manaSymbols = cleanedManaCost.Split(",");

                foreach (string symbol in manaSymbols)
                {
                    string symbolUrl = $"https://gatherer.wizards.com/Handlers/Image.ashx?type=symbol&name={symbol}&size=medium";
                    symbols.Add(symbolUrl);
                }
            }

            return symbols;
        }
    }
}

