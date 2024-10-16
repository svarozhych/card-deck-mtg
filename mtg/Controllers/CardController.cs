using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using mtg.Models;
using Newtonsoft.Json;
using System.Linq;
using mtg_lib.Library.Services;
using mtg.Library.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;

namespace mtg.Controllers;

public class CardController : Controller
{
    private CardHandler cardHandler = new CardHandler();

    [Authorize(Roles = "Admin, Member")]
    public IActionResult Index(string search, string cardType, string cardColor, int page = 1)
    {
        int pageSize = 20; // number of cards by page
        var cards = cardHandler.GetCards();


        if (!string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(cardType) || !string.IsNullOrEmpty(cardColor))
            cards = cardHandler.SearchWithFilter(search, cardType, cardColor);

        var totalItems = cards.Count();

        var items = cards.Skip((page - 1) * pageSize).Take(pageSize);

        CardIndexViewModel model = new CardIndexViewModel
        {
            Cards = items.ToList(),
            PageViewModel = new PageViewModel(totalItems, page, pageSize, search),
            Search = search,
            CardType = cardType,
            CardColor = cardColor
        };

        cardHandler.StorePreviousUrl(HttpContext.Session, Request.GetDisplayUrl());

        return View(model);
    }

    public IActionResult Back()
    {
        string previousUrl = cardHandler.GetPreviousUrl(HttpContext.Session);
        int index = previousUrl.LastIndexOf('/') + 4;
        string result = previousUrl.Substring(index + 1);
        string final = result.Substring(0, result.Length - 1);


        string decodedString = final.Replace("\\u0026", "&");


        if (!string.IsNullOrEmpty(decodedString))
        {
            cardHandler.ClearSession(HttpContext.Session);
            return Redirect("/Card" + decodedString);
        }

        cardHandler.ClearSession(HttpContext.Session);
        return RedirectToAction("Index");
    }




}
