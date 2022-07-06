using _2rp_processo.webApi.Domains;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _2rp_processo.webApi.Contexts
{
    public class Processo2rpContext : DbContext
    {

        public Processo2rpContext(DbContextOptions<Processo2rpContext> options)
            : base(options)
        {
        }

        public DbSet<TipoUsuario> TiposUsuarios { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"name=AwsRdsConnetionString");
            }
        }
    }
}
