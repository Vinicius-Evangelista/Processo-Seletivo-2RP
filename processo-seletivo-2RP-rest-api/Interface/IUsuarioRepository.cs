using _2rp_processo.webApi.Domains;
using _2rp_processo.webApi.ViewModels;
using System.Collections.Generic;

namespace _2rp_processo.webApi.Interface
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto contendo os dados do usuário</param>
        void CadastrarUsuario(UsuarioViewModelCadastro novoUsuario);

        /// <summary>
        /// Exclui um usuário existente
        /// </summary>
        /// <param name="idUsuario">Id do usuário que será excluido</param>
        void ExcluirUsuario(int idUsuario);

        /// <summary>
        /// Atualiza o email e a senha de um usuário
        /// </summary>
        /// <param name="idUsuario">Id do usuario que será atualizado</param>
        /// <param name="usuarioAtualizado">Objeto contendo um novo email e senha</param>
        Usuario AtualizarUsuarioGeral(int idUsuario, UsuarioAtualizadoGeralViewModel usuarioAtualizado);

        /// <summary>
        /// Atualiza um todas as informaçoes de um usuário
        /// </summary>
        /// <param name="idUsuario">Id do usuario que será atualizado</param>
        /// <param name="usuarioAtualizado">Novas informções do usuário que serão atualizadas</param>
        /// <returns></returns>
        Usuario AtualizarUsuarioPrivilegiado(int idUsuario, UsuarioAtualizadoPrivilegiado usuarioAtualizado);

        /// <summary>
        /// Lista todos os usuário
        /// </summary>
        /// <returns>Uma lista com todos os usuários</returns>
        List<Usuario> ListarUsuarios();

        /// <summary>
        /// Lista um usuário em específico
        /// </summary>
        /// <param name="idUsuario">Id do usuário que será listado</param>
        /// <returns>O usuário de acordo com o Id passado</returns>
        Usuario ListarUsuarioPorId(int idUsuario);

        /// <summary>
        /// Efetua o Login
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <param name="senha">Senha do usuario</param>
        /// <returns>O token do usuario</returns>
        Usuario Login(string email, string senha);

    }
}
