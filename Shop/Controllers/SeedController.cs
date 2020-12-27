using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{    
    [Route("")]
    public class SeedController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ActionResult<dynamic>>> Get([FromServices] DataContext context)
        {
            var setup = context.Setup.FirstOrDefault();

            if (setup != null)
            {
                return Ok("O sistema já foi configurado com as informações básicas, use o swagger para testar");
            }

            setup = new Setup();
            setup.InstallationDate = System.DateTime.Now;
            setup.SeedExecuted = true;
                        
            var categoriaLimpeza = context.Categories.Add(new Category() { Title = "Limpeza"});
            var categoriaPetisco = context.Categories.Add(new Category() { Title = "Petiscos"});
            
            await context.SaveChangesAsync();

            var detergente = context.Products.Add(new Product() { Price = 1, Title = "Detergente", CategoryId = categoriaLimpeza.Entity.Id });
            var torcida = context.Products.Add(new Product() { Price = 1.5M, Title = "Torcida 200g", CategoryId = categoriaPetisco.Entity.Id});

            var adm = context.User.Add(new User() { Password = "123", Role = "manager", Username = "adm" }).Entity;

            await context.SaveChangesAsync();

            var produtos = new List<Product>();
            produtos.Add(detergente.Entity);
            produtos.Add(torcida.Entity);

            var retorno = new { message = "Configuração basica criada com sucesso",
                produtos = produtos,
                user = adm
            };

            context.Setup.Add(setup);
            await context.SaveChangesAsync();

            return Ok(retorno);
        }
    }
}
