using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{    
    [Route("configure")]
    public class SeedController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var categoria = context.Categories.Where(x => x.Title == "Limpeza").FirstOrDefault();

            if(categoria != null)
            {
                return Ok("A configuração já foi executadada anteriormente, execução ignorada");
            }
            
            var categoriaLimpeza = context.Categories.Add(new Category() { Title = "Limpeza"});
            var categoriaPetisco = context.Categories.Add(new Category() { Title = "Petiscos"});

            await context.SaveChangesAsync();

            context.Products.Add(new Product() { Price = 1, Title = "Detergente", CategoryId = categoriaLimpeza.Entity.Id });
            context.Products.Add(new Product() { Price = 1.5M, Title = "Torcida 200g", CategoryId = categoriaPetisco.Entity.Id});

            context.User.Add(new User() { Password = "123", Role = "manager", Username = "adm" });

            await context.SaveChangesAsync();

            return "Configuração executada com sucesso";
        }
    }
}
