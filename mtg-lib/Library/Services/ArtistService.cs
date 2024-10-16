using mtg_lib.Library.Models;

namespace mtg_lib.Library.Services
{
    public class ArtistService
    {
        MtgContext dbMTG = new MtgContext();

        public string GetArtistForCard(long id)
        {
            string artistName = "";

            var tmpArtist = dbMTG.Cards?.Where(card => card.Id == id).First();

            if (tmpArtist != null)
            {
                var artist = dbMTG.Artists?.Where(artist => artist.Id == tmpArtist.ArtistId).First();
                if (!string.IsNullOrEmpty(artist?.FullName))
                {
                    artistName = artist.FullName;
                }
            }

            return artistName;
        }
    }
}