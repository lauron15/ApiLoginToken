using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLoginToken.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("senha")]
        public string? Senha { get; set; } = string.Empty;

        [Column("Email")]
        public string? Email { get; set; } = string.Empty;

        [Column("roles")]
        public string[] Roles { get; set; } = new string[] { };

    }
}
