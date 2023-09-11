using Rumrejsen.Models;

namespace Rumrejsen.Interfaces
{
    public interface IGalacticRouteDatahandler
    {
        public GalacticRouteList GetGalacticRouteList(ApiKey apiKey);
    }
}
