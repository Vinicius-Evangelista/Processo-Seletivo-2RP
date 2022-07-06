using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2rp_processo.webApi.Domains
{
    [Table("TipoUsuario")]
    public class TipoUsuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoUsuario { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string NomeTipoUsuario { get; set; }
    }
}
