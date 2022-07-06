using _2rp_processo.webApi.Domains;
using System.Collections.Generic;

namespace _2rp_processo.webApi.Interface
{
    public interface ITipoUsuarioRepository
    {
        /// <summary>
        /// LIsta todos os tipos usuarios
        /// </summary>
        /// <returns>Uma lista com todos os tipos de usuarios</returns>
        List<TipoUsuario> ListarTipoUsuario();

    }
}
