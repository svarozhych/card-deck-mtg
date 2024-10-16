using mtg_lib.Library.Models;

namespace mtg_lib.Library.Services
{
    public class CardColorService
    {
        MtgContext dbMTG = new MtgContext();

        public string GetColorForCard(long CardId)
        {
            string colorName = "";

            var tmpColor = dbMTG.CardColors?.Where(cardcolor => cardcolor.CardId == CardId).First();

            if (tmpColor != null)
            {
                var color = dbMTG.Colors?.Where(color => color.Id == tmpColor.ColorId).First();
                if (!string.IsNullOrEmpty(color?.Name))
                {
                    colorName = color.Name;
                }
            }

            return colorName;
        }
    }
}