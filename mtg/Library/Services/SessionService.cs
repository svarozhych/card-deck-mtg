using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;
using mtg.Models;


namespace mtg.Library.Services
{
    public class SessionService
    {
        public void StorePreviousUrl(ISession session, string url)
        {
            storeSessionData(session, "previousUrl", url);
        }

        public string GetPreviousUrl(ISession session)
        {
            var url = "";
            if (session.GetString("previousUrl") != null)
                url = session.GetString("previousUrl");
            return url;
        }


        public void ClearSession(ISession session)
        {
            session.Clear();
        }

        private void storeSessionData(ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}
