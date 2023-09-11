using Rumrejsen.DAL;
using Rumrejsen.Interfaces;
using Rumrejsen.Models;

namespace Rumrejsen
{
    public class Manager
    {
        // Dependency injection would probably have made more sense, but wasn't implemented in this small example
        private GalacticRouteDataHandler galacticRouteDataHandler = new GalacticRouteDataHandler();

        public List<GalacticRoute> GetGalacticRouteList(string apiKeyValue)
        {
            ApiKey apiKey = ApiKeys.apiKeys.FirstOrDefault(k => k.ApiKeyValue == apiKeyValue)!;
            return galacticRouteDataHandler.GetGalacticRouteList(apiKey).galacticRoutes;
        }
    }
}
