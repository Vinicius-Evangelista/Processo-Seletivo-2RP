using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _2rp_processo.webApi.Domains
{

    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public int IdTipoUsuario { get; set; }

        [JsonIgnore]
        [ForeignKey("IdTipoUsuario")]
        public TipoUsuario TipoUsuario { get; set; }

        [Column(TypeName =  "varchar(100)")]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column(TypeName =  "varchar(100)")]
        public string Senha { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Foto { get; set; }

        [Column(TypeName = "bit")]
        public bool Status { get; set; }


    }
}
