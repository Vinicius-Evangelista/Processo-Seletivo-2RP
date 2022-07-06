using _2rp_processo.webApi.Contexts;
using _2rp_processo.webApi.Domains;
using _2rp_processo.webApi.Interface;
using System.Collections.Generic;
using System.Linq;

namespace _2rp_processo.webApi.Repository
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly Processo2rpContext ctx;

        public TipoUsuarioRepository(Processo2rpContext appContex)
        {
            ctx = appContex;
        }

        public List<TipoUsuario> ListarTipoUsuario()
        {
            return ctx.TiposUsuarios.ToList();
        }
    }
}
