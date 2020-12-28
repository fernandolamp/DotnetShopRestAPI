using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{    
    [Route("configuration")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public async Task<ActionResult<ActionResult<dynamic>>> Get([FromServices] DataContext context)
        {
            var url = $"Hello World!  {Request.GetDisplayUrl()}swagger";
            return Ok(url);
        }
    }
}
