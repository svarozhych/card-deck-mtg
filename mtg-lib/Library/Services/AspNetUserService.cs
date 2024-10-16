using mtg_lib.Library.Services;
using mtg_lib.Library.Models;

namespace mtg_lib.Library.Services
{
    public class AspNetUserService
    {
        MtgContext dbMTG = new MtgContext();

        public string GetUserId(string username)
        {

            string? userId = dbMTG?.AspNetUsers?.FirstOrDefault(user => user.UserName == username)?.Id;
            if (userId != null)
                return userId;

            return "/";
        }
    }
}