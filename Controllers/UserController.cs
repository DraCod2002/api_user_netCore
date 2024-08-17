using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUser.Models;
using Microsoft.EntityFrameworkCore;

namespace WebUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbuserContext dbContext;
        public UserController(DbuserContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult> Get()
        {
            var listaUser = await dbContext.Usuarios.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaUser);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(e => e.Id == id);
            return StatusCode(StatusCodes.Status200OK, usuario);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<ActionResult> Nuevo([FromBody] Usuario objeto)
        {
            await dbContext.Usuarios.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult> Editar([FromBody] Usuario objeto)
        {
            dbContext.Usuarios.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "User not found" });
            }

            if (user.Password != request.CurrentPassword)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Current password is incorrect" });
            }

            user.Password = request.NewPassword;
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Password changed successfully" });
        }
    }

    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
