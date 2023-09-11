using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Rumrejsen.DAL;
using Rumrejsen.Interfaces;
using Rumrejsen.Models;

namespace Rumrejsen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalacticRouteController : ControllerBase
    {
        Manager mana = new Manager();

        [HttpGet]
        public ActionResult<List<GalacticRoute>> Get()
        {

            if (HttpContext.Request.Headers.TryGetValue("XApiKey", out StringValues extractedApiKey))
            {
                return StatusCode(200, mana.GetGalacticRouteList(extractedApiKey!));
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}
