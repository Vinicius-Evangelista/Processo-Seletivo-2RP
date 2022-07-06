
using _2rp_processo.webApi.Domains;
using _2rp_processo.webApi.Interface;
using _2rp_processo.webApi.Utils;
using _2rp_processo.webApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _2rp_processo.webApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository repo)
        {
            _usuarioRepository = repo;
        }

        [Authorize(Roles = "1,2")]
        [HttpPost("Cadastrar")]
        public IActionResult CadastrarUsuario( [FromForm] UsuarioViewModelCadastro NovoUsuario, IFormFile FotoPerfil)
        {
            try
            {
                if(NovoUsuario == null)
                    return BadRequest("O Objeto não pode estar vazio!");

                if (FotoPerfil == null)
                {
                    NovoUsuario.Foto = "imagem-padrao.png";
                }
                else
                {
                    #region Upload da Imagem com extensões permitidas apenas
                    string uploadResultado = Upload.UploadFile(FotoPerfil).ToString();

                    if (uploadResultado == "")
                    {
                        return BadRequest("Arquivo não encontrado !");
                    }
                    if (uploadResultado == "Extensão não permitida")
                    {
                        return BadRequest("Extensão do arquivo não permitida");
                    }

                    NovoUsuario.Foto = uploadResultado;
                    #endregion
                }


                _usuarioRepository.CadastrarUsuario(NovoUsuario);
                return StatusCode(201);
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("Listar")]
        public IActionResult ListarUsuarios()
        {
            try
            {
                return Ok(_usuarioRepository.ListarUsuarios());
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("Listar/{idUsuario}")]
        public IActionResult ListaUsuarioPorId(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    return BadRequest("O id do Usuário não pode ser 0 !");
                }

                return Ok(_usuarioRepository.ListarUsuarioPorId(idUsuario));
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPut("Atualizar/Privilegiado/{IdUsuario}")]
        public IActionResult AtualizarUsuarioPrivilegiado(int IdUsuario, [FromForm] UsuarioAtualizadoPrivilegiado UsuarioAtualizado, IFormFile FotoPerfil)
        {
            try
            {
                if (IdUsuario == 0)
                    return BadRequest("Não há nenhum usuário com Id 0");

                else if (UsuarioAtualizado == null)
                    return BadRequest("O Objeto não pode estar vazio!");

                if (FotoPerfil != null)
                {

                    #region Upload da Imagem com extensões permitidas apenas
                    var usuarioAchado = _usuarioRepository.ListarUsuarioPorId(IdUsuario);

                    string uploadResultado = Upload.AtualizarArquivo(usuarioAchado.Foto,FotoPerfil).ToString();
                    
                    if (uploadResultado == "")
                    {
                        return BadRequest("Arquivo não encontrado !");
                    }
                    if (uploadResultado == "Extensão não permitida")
                    {
                        return BadRequest("Extensão do arquivo não permitida");
                    }

                    UsuarioAtualizado.Foto = uploadResultado;
                    #endregion
                }
                return Ok(_usuarioRepository.AtualizarUsuarioPrivilegiado(IdUsuario, UsuarioAtualizado));
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpPut("Atualizar/Geral/{IdUsuario}")]
        public IActionResult AtualizarUsuarioGeral(int IdUsuario, [FromForm] UsuarioAtualizadoGeralViewModel UsuarioAtualizado, IFormFile FotoPerfil)
        {
            try
            {

                if (IdUsuario == 0)
                    return BadRequest("Não há nenhum usuário com Id 0");

                else if (UsuarioAtualizado == null)
                    return BadRequest("O Objeto não pode estar vazio!");

                if (FotoPerfil != null)
               {

                    #region Upload da Imagem com extensões permitidas apenas
                    var usuarioAchado = _usuarioRepository.ListarUsuarioPorId(IdUsuario);

                    string uploadResultado = Upload.AtualizarArquivo(usuarioAchado.Foto, FotoPerfil).ToString();

                    if (uploadResultado == "")
                    {
                        return BadRequest("Arquivo não encontrado !");
                    }
                    if (uploadResultado == "Extensão não permitida")
                    {
                        return BadRequest("Extensão do arquivo não permitida");
                    }

                    UsuarioAtualizado.Foto = uploadResultado;
                    #endregion
                }
                return Ok(_usuarioRepository.AtualizarUsuarioGeral(IdUsuario, UsuarioAtualizado));
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }
        }


        [Authorize(Roles = "1")]
        [HttpDelete("Excluir/{IdUsuario}")]
        public IActionResult ExcluirUsuario(int IdUsuario)
        {
            try
            {
                if (IdUsuario == 0)
                    return BadRequest("Não há nenhum usuário com Id 0");

                _usuarioRepository.ExcluirUsuario(IdUsuario);
                return NoContent();
            }
            catch (Exception execp)
            {
                return BadRequest(execp);
            }
        }
    }
}
