using mtg_lib.Library.Services;
using mtg_lib.Library.Models;
using mtg.Models;

namespace mtg.Library.Handlers
{
    public class DetailsHandler

    {
        private CardService cardService = new CardService();
        private CardColorService colorService = new CardColorService();
        private SetService setService = new SetService();
        private RarityService rarityService = new RarityService();
        private ArtistService artistService = new ArtistService();


        public CardDetails CreateDetails(long id)
        {
            Card card = cardService.GetCardById(id);
            return new CardDetails
            {
                Id = card.Id,
                Name = card.Name,
                Set = setService.GetSetForCard(id),
                ManaCost = card.ManaCost,
                Type = card.Type,
                Text = card.Text,
                Color = colorService.GetColorForCard(id),
                OriginalImageUrl = card.OriginalImageUrl,
                Rarity = rarityService.GetRarityForCard(id),
                Artist = artistService.GetArtistForCard(id),
                Flavor = card.Flavor,
                Power = card.Power,
                Toughness = card.Toughness,
                Number = card.Number,
                Symbol = "https://gatherer.wizards.com/Handlers/Image.ashx?type=symbol&set=" + card.SetCode + "&size=large&rarity=" + rarityService.GetRarityForCard(id).Substring(0, 1),
                nextId = id + 1,
                prevId = id - 1
            };
        }
    }
}