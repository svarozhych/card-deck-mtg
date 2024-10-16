using Microsoft.AspNetCore.Mvc;
using mtg.Library.Handlers;
using mtg.Models;
using System.Diagnostics;
using System;
using mtg_lib.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace mtg.Controllers
{
    public class DecksController : Controller
    {
        private DecksHandler decksHandler = new DecksHandler();
        private CardHandler cardHandler = new CardHandler();

        [Authorize(Roles = "Admin, Member")]
        public IActionResult Index()
        {
            var username = User?.Identity?.Name;
            var decks = new List<mtg_lib.Library.Models.Deck>();
            if (username != null)
            {
                var id = decksHandler.GetUserId(username);
                decks = decksHandler.GetDecksForUser(id);
            }

            return View(decks);
        }

        [Authorize(Roles = "Admin, Member")]
        public IActionResult Details(int Id)
        {
            Deck deck = decksHandler.GetDeckWithId(Id);

            var username = User?.Identity?.Name;

            if (username != null)
                if (deck == null || deck.UserId != decksHandler.GetUserId(username))
                {
                    return RedirectToAction("Error");
                }

            DeckDetails deckDetail = decksHandler.GetDeckDetails(deck);

            return View(deckDetail);
        }

        [Authorize(Roles = "Admin, Member")]
        public IActionResult Create(int page = 1)
        {
            DeckViewModel model = new DeckViewModel();
            var cards = cardHandler.GetCards();
            int pageSize = 20;
            /*
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(cardType) || !string.IsNullOrEmpty(cardColor))
                cards = cardHandler.SearchWithFilter(search, cardType, cardColor);
            */
            var totalItems = cards.Count();

            var items = cards.Skip((page - 1) * pageSize).Take(pageSize);

            string search = "";
            string cardType = "";
            string cardColor = "";

            CardIndexViewModel cardView = new CardIndexViewModel
            {
                Cards = items.ToList(),
                PageViewModel = new PageViewModel(totalItems, page, pageSize, search),
                Search = search,
                CardType = cardType,
                CardColor = cardColor
            };
            model.AvailableCards = cardView;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(DeckViewModel model)
        {

            if (ModelState.IsValid)
            {
                string? name = model.Name;
                string? description = model.Description;
                int[]? selectedCardIds = model.SelectedCardIds;
                string? UserId = null;
                if (User?.Identity?.Name != null)
                {
                    UserId = decksHandler.GetUserId(User.Identity.Name);
                }
                bool hasRepeatedNumber = false;

                if (selectedCardIds != null)
                    foreach (int number in selectedCardIds)
                    {
                        if (selectedCardIds.Count(n => n == number) > 4)
                        {
                            hasRepeatedNumber = true;
                            break;
                        }
                    }
                if (!hasRepeatedNumber)
                {
                    if (UserId != null && name != null)
                        if (decksHandler.IsNameAvailable(UserId, name))
                        {

                            if (selectedCardIds != null && name != null && description != null)
                            {


                                var deck = new Deck
                                {
                                    Name = name,
                                    Description = description,
                                    CardsId = selectedCardIds,
                                    UserId = UserId
                                };

                                decksHandler.CreateNewDeck(deck);

                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ModelState.AddModelError("DeckSize", "Deck cannot be empty and must possess a name and a description.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("DeckSize", "Deck name already used");
                        }
                }
                else
                {
                    ModelState.AddModelError("DeckSize", "Deck cannot contain the same card more than 4 times");
                }
            }
            else
            {
                ModelState.AddModelError("DeckSize", "Deck should contain 60 cards");
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                errorMessages.ForEach(mess => Console.WriteLine(mess));
            }

            var cards = cardHandler.GetCards();
            int pageSize = 20;
            int page = 1;

            var totalItems = cards.Count();

            var items = cards.Skip((page - 1) * pageSize).Take(pageSize);

            string search = "";
            string cardType = "";
            string cardColor = "";

            CardIndexViewModel cardView = new CardIndexViewModel
            {
                Cards = items.ToList(),
                PageViewModel = new PageViewModel(totalItems, page, pageSize, search),
                Search = search,
                CardType = cardType,
                CardColor = cardColor
            };
            model.AvailableCards = cardView;
            return View(model);

        }

        public IActionResult Remove(int deckId)
        {
            decksHandler.RemoveDeck(deckId);
            return RedirectToAction("Index");
        }


        public IActionResult Error()
        {
            return View("Error");
        }
    }
}