using mtg_lib.Library.Models;

namespace mtg_lib.Library.Services
{
    public class RarityService
    {
        MtgContext dbMTG = new MtgContext();

        public string GetRarityForCard(long id)
        {
            string rarityName = "";

            var tmpRarity = dbMTG.Cards?.Where(card => card.Id == id).First();

            if (tmpRarity != null)
            {
                var rarity = dbMTG.Rarities?.Where(rarity => rarity.Code == tmpRarity.RarityCode).First();
                if (!string.IsNullOrEmpty(rarity?.Name))
                {
                    rarityName = rarity.Name;
                }
            }

            return rarityName;
        }
    }
}