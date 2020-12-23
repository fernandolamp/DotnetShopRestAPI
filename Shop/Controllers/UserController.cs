using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        [Route("")]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.User.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            var user = await context.User.AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválido" });

            var token = TokenService.GenerateToken(user);

            return new
            {
                user = user,
                token = token
            };

        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] DataContext context)
        {
            var users = await context.User.AsNoTracking().ToListAsync();
            return users;
        }

        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put([FromServices] DataContext context, int id, [FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != model.Id)
            {
                return BadRequest("Usuário não encontrado");
            }

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest("Não foi possível salvar as alterações");
            }
        }
    }
}
