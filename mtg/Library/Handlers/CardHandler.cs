using mtg_lib.Library.Services;
using mtg_lib.Library.Models;
using mtg.Library.Services;

namespace mtg.Library.Handlers
{
    public class CardHandler
    {
        private CardService cardService = new CardService();
        private SessionService sessionService = new SessionService();

        public List<Card> GetCards()
        {
            var cards = cardService.GetAllCards();
            return cards;
        }

        public List<Card> Search(string search)
        {
            var cards = cardService.Search(search);
            return cards;
        }

        public List<Card> SearchWithFilter(string search, string type, string color)
        {
            var cards = cardService.SearchWithFilter(search, type, color);
            return cards;
        }
        public void StorePreviousUrl(ISession session, string url)
        {
            sessionService.StorePreviousUrl(session, url);
        }

        public string GetPreviousUrl(ISession session)
        {
            return sessionService.GetPreviousUrl(session);
        }

        public void ClearSession(ISession session)
        {
            sessionService.ClearSession(session);
        }
    }
}