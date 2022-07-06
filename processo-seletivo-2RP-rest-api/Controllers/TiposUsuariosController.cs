using _2rp_processo.webApi.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2rp_processo.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposUsuariosController : ControllerBase
    {
        // GET: api/<TiposUsuariosController>
        private readonly ITipoUsuarioRepository _TipoUsuarioRepository;

        public TiposUsuariosController(ITipoUsuarioRepository repo)
        {
            _TipoUsuarioRepository = repo;
        }

        // GET: api/<idTipoUsuariosController>
        //[Authorize(Roles = "2, 3")]
        [HttpGet("Listar")]
        public IActionResult ListarTiposUsuarios()
        {
            try
            {
                return Ok(_TipoUsuarioRepository.ListarTipoUsuario());
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            };
        }
    }
}
