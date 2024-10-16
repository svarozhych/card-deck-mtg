using mtg_lib.Library.Models;

namespace mtg_lib.Library.Services
{
    public class SetService
    {
        MtgContext dbMTG = new MtgContext();

        public string GetSetForCard(long id)
        {
            string setName = "";

            var tmpSet = dbMTG.Cards?.Where(card => card.Id == id).First();

            if (tmpSet != null)
            {
                var set = dbMTG.Sets?.Where(set => set.Code == tmpSet.SetCode).First();
                if (!string.IsNullOrEmpty(set?.Name))
                {
                    setName = set.Name;
                }
            }

            return setName;
        }
    }
}