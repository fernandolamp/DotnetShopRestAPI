using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Controllers
{    
    [Route("v1/categories")]
    public class CategoryController:ControllerBase
    {        
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [ResponseCache(VaryByHeader ="User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categories.AsNoTracking().ToListAsync();
            return categorias;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
        {
            var categoria = await context.Categories.AsNoTracking().FirstOrDefaultAsync();
            if (categoria == null)
               return NotFound(new { message = "Categoria não encontrada" });

            return categoria;
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager")]
        [Route("")]
        public async Task<ActionResult<Category>> Post(
            [FromBody] Category model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a categoria" });
            }            
            
        }

        [HttpDelete]
        [Authorize(Roles = "employee,manager")]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
            {
                return NotFound(new { message = "Categoria não encontrada" });
            }

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível remover a categoria" });
            }            
        }

        [HttpPut]
        [Authorize(Roles = "employee,manager")]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id,
            [FromBody] Category model,
            [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Esse registro já foi atualizado" });
            }
            catch
            {
                return BadRequest(new { message = "Ocorreu um erro ao tentar atualizar a categoria" });
            }
                        
        }

    }
}
