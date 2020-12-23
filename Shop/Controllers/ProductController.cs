using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]        
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context) {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            //var products = await context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetProdcutsByCategoryId([FromServices] DataContext context, int id)
        {
            var products = await context.Products
                .Include(x => x.Category)
                .AsNoTracking().
                Where(x => x.CategoryId == id)
                .ToListAsync();

            if(products == null)
            {
                return BadRequest(new { message = "Produto não encontrado" });
            }

            return Ok(products);
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(product);
        }

        [HttpPut]
        [Authorize(Roles = "employee")]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Put([FromServices] DataContext context, int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Product>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "O registro já foi atualizado" });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "employee")]
        [Route("id:int")]
        public async Task<ActionResult> Delete([FromServices] DataContext context, int id)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
                context.Remove(product);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível deletar o produto" });
            }        
        }

        [HttpPost]
        [Authorize(Roles = "employee")]
        [Authorize]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(product);
            }

            try
            {               
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return Ok(product);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o produto"});
            }
        }
    }
}
