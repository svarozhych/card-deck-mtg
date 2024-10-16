using mtg_lib.Library.Models;
using mtg_lib.Library.Services;

namespace mtg_lib.Library.Services
{
    public class CardService
    {
        MtgContext dbMTG = new MtgContext();
        CardColorService colorService = new CardColorService();

        public Card GetFirstCard()
        {
            var cards = GetAllCards();
            return cards.First();
        }

        public List<Card> GetAllCards()
        {
            var cards = new List<Card>();
            if (dbMTG.Cards != null)
            {
                cards = dbMTG.Cards.ToList();
            }
            return cards;
        }

        public List<Card> Search(string search)
        {
            var cards = new List<Card>();
            if (dbMTG.Cards != null)
            {
                cards = dbMTG.Cards.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return cards;
        }


        public List<Card> SearchWithFilter(string search, string? type, string? color)
        {
            if (dbMTG.Cards != null)
            {
                var cards = dbMTG.Cards.AsQueryable();

                if (search != null)
                {
                    cards = cards.Where(c => c.Name.ToLower().Contains(search.ToLower()));
                }
                if (type != null)
                {
                    cards = cards.Where(c => c.Type.ToLower().Contains(type.ToLower()));
                }
                if (color != null)
                {
                    if (dbMTG.CardColors != null && dbMTG.Colors != null)
                    {
                        cards = cards.Join(dbMTG.CardColors, c => c.Id, cc => cc.CardId, (c, cc) => new { Card = c, ColorId = cc.ColorId })
                        .Join(dbMTG.Colors, cc => cc.ColorId, cl => cl.Id, (cc, cl) => new { Card = cc.Card, Color = cl.Name })
                        .Where(c => c.Color.ToLower().Contains(color.ToLower()))
                        .Select(c => c.Card);
                    }
                }

                return cards.ToList();
            }
            return new List<Card>();
        }

        public Card GetCardById(long id)
        {

            var card = dbMTG.Cards?.FirstOrDefault(c => c.Id == id);

            if (card != null)
                return card;

            return new Card();
        }
    }
}