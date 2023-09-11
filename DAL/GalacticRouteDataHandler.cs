using Newtonsoft.Json;
using Rumrejsen.Interfaces;
using Rumrejsen.Models;
using System.Diagnostics;

namespace Rumrejsen.DAL
{
    public class GalacticRouteDataHandler : IGalacticRouteDatahandler
    {
        /// <summary>
        /// Converts a json string to a list of galactic routes
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public GalacticRouteList GetGalacticRouteList(ApiKey apiKey)
        {
            GalacticRouteList galacticRouteList = new GalacticRouteList();

            string jsonText = GetJsonText();

            if (jsonText.Equals(""))
            {
                return new GalacticRouteList();
            }
            else
            {
                galacticRouteList = JsonConvert.DeserializeObject<GalacticRouteList>(jsonText)!;

                if (apiKey.IsCaptain)
                {
                    return galacticRouteList;
                }
                else
                {
                    galacticRouteList.galacticRoutes = galacticRouteList.galacticRoutes.Where(x => !x.duration.Contains("år")).ToList();
                    return galacticRouteList;
                }
            }
        }

        /// <summary>
        /// Reads through a local file to get a json formatted string with galactic routes and their properties simulating a database call
        /// </summary>
        /// <returns></returns>
        private string GetJsonText()
        {
            string jsonText = "";

            try
            {
                using (StreamReader r = new StreamReader("C:\\Users\\mpp\\Downloads\\galacticRoutes.txt"))
                {
                    jsonText = r.ReadToEnd();
                }

                return jsonText;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Streamreader failed with the following message:\n" + ex);
                return jsonText;
            }
        }
    }
}
