using mtg_lib.Library.Services;
using mtg_lib.Library.Models;

namespace mtg_lib.Library.Services
{
    public class DecksService
    {
        MtgContext dbMTG = new MtgContext();

        public List<Deck> GetAllDecks()
        {
            var decks = dbMTG?.Decks?.ToList();
            if (decks != null)
                return decks;

            return new List<Deck>();
        }

        public List<Deck> GetAllDecksForUser(string userId)
        {
            var decks = dbMTG?.Decks?.Where(d => d.UserId == userId).ToList();
            if (decks != null)
                return decks;

            return new List<Deck>();
        }

        public Deck GetDeckWithId(int Id)
        {
            if (Id != 0 && DeckExist(Id))
            {
                var deck = dbMTG?.Decks?.Where(deck => deck.Id == Id).First();
                if (deck != null)
                    return deck;
            }

            return new Deck();
        }

        public void CreateNewDeck(Deck deck)
        {
            dbMTG?.Decks?.Add(deck);
            dbMTG?.SaveChanges();
        }

        public bool IsNameAvailable(string userId, string name)
        {
            var decks = GetAllDecksForUser(userId);
            foreach (var deck in decks)
            {
                if (deck.Name == name)
                    return false;
            }
            return true;
        }

        public void RemoveDeck(int deckId)
        {
            var deck = GetDeckWithId(deckId);

            if (deck != null)
            {
                dbMTG?.Decks?.Remove(deck);
                dbMTG?.SaveChanges();
            }
            else
            {
                throw new Exception("Deck not found");
            }
        }

        public bool DeckExist(int id)
        {
            if (dbMTG?.Decks?.Where(deck => deck.Id == id).Count() > 0)
                return true;
            return false;
        }
    }
}