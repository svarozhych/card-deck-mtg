using Microsoft.AspNetCore.Mvc;
using mtg.Library.Handlers;
using mtg.Models;

namespace mtg.Controllers
{
    public class DetailsController : Controller
    {
        DetailsHandler detailsHandler = new DetailsHandler();

        public IActionResult Index(long id)
        {
            if (id == 0 || id > 65597)
            {
                return RedirectToAction("Error");
            }
            CardDetails details = detailsHandler.CreateDetails(id);
            return View(details);
        }

        public IActionResult Error()
        {
            return View("Error");
        }
    }
}