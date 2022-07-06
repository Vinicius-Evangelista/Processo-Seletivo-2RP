using _2rp_processo.webApi.Domains;
using _2rp_processo.webApi.Interface;
using _2rp_processo.webApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace _2rp_processo.webApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController(IUsuarioRepository repo)
        {
            _usuarioRepository = repo;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public IActionResult Logar(LoginViewModel login)
        {
            try
            {
                //buscando um usuário
                Usuario usuarioBuscado = _usuarioRepository.Login(login.Email, login.Senha);

                if (usuarioBuscado != null)
                {

                    //declarando as claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                        new Claim("role", usuarioBuscado.IdTipoUsuario.ToString()),
                        new Claim("foto", usuarioBuscado.Foto),
                        new Claim("nome", usuarioBuscado.Nome),
                        new Claim("status", usuarioBuscado.Status.ToString()),
                    };

                    //key word para descriptografar a informação
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("LKAJSDÇLKFHISFDJA-AHEFHLAEHFL-HFEUH298HG3RQ2"));

                    //encriptografando os dados
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //declarando atributos do token
                    var MeuToken = new JwtSecurityToken(

                        issuer: "2rp-processo.webApi",
                        audience: "2rp-processo.webApi",

                        //claims/informações
                        claims: minhasClaims,

                        //data de expiração
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                        );

                    //escrevendo o token e devolvedo
                    return Ok(new
                    {
                        //escrever o token
                        token = new JwtSecurityTokenHandler().WriteToken(MeuToken)
                    }
                        );
                }

                return NotFound(
                    new
                    {
                        mensagem = "Email/Senha passados estão incorretos ou o usuário não existe !",
                        erro = true
                    }

                    );
            }
            catch (Exception excep)
            {
                return BadRequest(
                    new
                    {
                        mensagem = "Algo deu errado no servidor !",
                        erro = excep
                    }
                    );
            }
        }
    }
}
