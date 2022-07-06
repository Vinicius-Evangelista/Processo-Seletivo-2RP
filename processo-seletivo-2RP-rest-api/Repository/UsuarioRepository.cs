using _2rp_processo.webApi.Contexts;
using _2rp_processo.webApi.Domains;
using _2rp_processo.webApi.Interface;
using _2rp_processo.webApi.Utils;
using _2rp_processo.webApi.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2rp_processo.webApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Processo2rpContext ctx;

        public UsuarioRepository(Processo2rpContext appContext)
        {
            ctx = appContext;
        }

        public Usuario AtualizarUsuarioPrivilegiado(int idUsuario, UsuarioAtualizadoPrivilegiado usuarioAtualizado)
        {
            var usuarioAchado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            if (usuarioAchado != null)
            {

                if (usuarioAtualizado.Nome != null)
                    usuarioAchado.Nome = usuarioAtualizado.Nome;

                if (usuarioAtualizado.Foto != null)
                    usuarioAchado.Foto = usuarioAtualizado.Foto;

                if (usuarioAtualizado.Senha != null)
                    usuarioAchado.Senha = usuarioAtualizado.Senha;

                if (usuarioAtualizado.Email != null)
                    usuarioAchado.Email = usuarioAtualizado.Email;

                if (usuarioAchado.Status != usuarioAtualizado.Status)
                    usuarioAchado.Status = usuarioAtualizado.Status;

                if (usuarioAtualizado.IdTipoUsuario != 0)
                    usuarioAchado.IdTipoUsuario = usuarioAtualizado.IdTipoUsuario;


                ctx.Usuarios.Update(usuarioAchado);
                ctx.SaveChanges();

                return usuarioAchado;
            }

            return null;
        }

        public Usuario AtualizarUsuarioGeral(int idUsuario, UsuarioAtualizadoGeralViewModel usuarioAtualizado)
        {
            var usuarioAchado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            if (usuarioAchado != null)
            {
                if (usuarioAtualizado.Senha != null)
                    usuarioAchado.Senha = usuarioAtualizado.Senha;

                if (usuarioAtualizado.Email != null)
                    usuarioAchado.Email = usuarioAtualizado.Email;

                if (usuarioAtualizado.Nome != null)
                    usuarioAchado.Nome = usuarioAtualizado.Nome;

                if (usuarioAtualizado.Foto != null)
                    usuarioAchado.Foto = usuarioAtualizado.Foto;

                ctx.Usuarios.Update(usuarioAchado);
                ctx.SaveChanges();

                return usuarioAchado;
            }

            return null;
        }

        public void CadastrarUsuario(UsuarioViewModelCadastro novoUsuario)
        {
            Usuario usuario = new()
            {
                Email = novoUsuario.Email,
                Foto = novoUsuario.Foto,
                IdTipoUsuario = novoUsuario.IdTipoUsuario,
                Nome = novoUsuario.Nome,
                Senha = novoUsuario.Senha,
                Status = novoUsuario.Status,
            };

            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
        }

        public void ExcluirUsuario(int idUsuario)
        {
            var usuarioAchado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario); // Procura um usuário na lista de usuários

            if (usuarioAchado != null)
            {
                ctx.Usuarios.Remove(usuarioAchado);
                ctx.SaveChanges();
            }
        }

        public List<Usuario> ListarUsuarios()
        {
            return ctx.Usuarios
                .Select(u => new Usuario
                {
                    IdUsuario = u.IdUsuario,
                    IdTipoUsuario = u.IdTipoUsuario,
                    Email = u.Email,
                    Senha = u.Senha,
                    Foto = u.Foto,
                    Nome = u.Nome,
                    Status = u.Status,
                    TipoUsuario = new TipoUsuario()
                    {
                        NomeTipoUsuario = u.TipoUsuario.NomeTipoUsuario,
                    }
                })
                .ToList();
        }

        public Usuario Login(string email, string senha)
        {
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario != null)
            {
                if (ValidarSenha(senha))
                {
                    // senha criptografada
                    string senhaHash = Criptografia.CriptografarSenha(senha);
                    usuario.Senha = senhaHash;
                    ctx.Usuarios.Update(usuario);
                    ctx.SaveChanges();
                }

                // comparada senha que fornecida pelo usuário com a senha que já está criptografa no banco
                bool confere = Criptografia.CompararSenha(senha, usuario.Senha);

                // caso a comparação seja válida retorne o usuário
                if (confere)
                    return usuario;


            }
            return null;
        }

        /// <summary>
        /// Atualiza a senha 
        /// </summary>
        /// <param name="idUsuario">Id do Usuario que a senha será atualizada</param>
        /// <param name="senhaAtualizada">senha atualizada</param>
        public void AtualizarSenha(int idUsuario, string senhaAtualizada)
        {
            Usuario usuario = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            if (ValidarSenha(senhaAtualizada))
            {
                // senha criptografada
                string senhaHash = Criptografia.CriptografarSenha(senhaAtualizada);
                usuario.Senha = senhaHash;
                ctx.Usuarios.Update(usuario);
                ctx.SaveChanges();
            }

        }

        public Usuario ListarUsuarioPorId(int idUsuario)
        {
            return ctx.Usuarios
                .Select(u => new Usuario
                {
                    IdUsuario = u.IdUsuario,
                    IdTipoUsuario = u.IdTipoUsuario,
                    Email = u.Email,
                    Senha = u.Senha,
                    Foto = u.Foto,
                    Nome = u.Nome,
                    Status = u.Status,
                    TipoUsuario = new TipoUsuario()
                    {
                        NomeTipoUsuario = u.TipoUsuario.NomeTipoUsuario,
                    }
                })
                .FirstOrDefault(u => u.IdUsuario == idUsuario);
        }

        /// <summary>
        /// Valida senha 
        /// </summary>
        /// <param name="senha">senha que será validada</param>
        /// <returns>se a senha é valida(true) ou não(false)</returns>
        public bool ValidarSenha(string senha)
        {
            const int TAMANHO = 60;

            if (string.IsNullOrEmpty(senha) || senha.Length > TAMANHO)
            {
                return false;
            }
            else if (Regex.IsMatch(senha, @"\$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
