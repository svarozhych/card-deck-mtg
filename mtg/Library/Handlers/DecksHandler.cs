using mtg_lib.Library.Services;
using mtg_lib.Library.Models;
using mtg.Models;

namespace mtg.Library.Handlers
{
    public class DecksHandler
    {
        private DecksService deckService = new DecksService();
        private AspNetUserService userService = new AspNetUserService();
        private CardService cardService = new CardService();


        public List<Deck> GetAllDecks()
        {
            var decks = deckService.GetAllDecks();
            return decks;
        }

        public List<Deck> GetDecksForUser(string userId)
        {
            var decks = deckService.GetAllDecksForUser(userId);
            return decks;
        }

        public string GetUserId(string username)
        {
            var id = userService.GetUserId(username);
            return id;
        }

        public Deck GetDeckWithId(int id)
        {
            var deck = deckService.GetDeckWithId(id);
            return deck;
        }

        public DeckDetails GetDeckDetails(Deck deck)
        {
            List<Card> cards = new List<Card>();

            foreach (int id in deck.CardsId)
            {
                Card card = cardService.GetCardById(id);
                if (card != null)
                {
                    cards.Add(card);
                }
            }

            return new DeckDetails
            {
                Id = deck.Id,
                Name = deck.Name,
                Description = deck.Description,
                Cards = cards,
                UserId = deck.UserId
            };
        }

        public void CreateNewDeck(Deck deck)
        {
            deckService.CreateNewDeck(deck);
        }

        public bool IsNameAvailable(string userId, string name)
        {
            return deckService.IsNameAvailable(userId, name);
        }

        public void RemoveDeck(int deckId)
        {
            deckService.RemoveDeck(deckId);
        }
    }
}