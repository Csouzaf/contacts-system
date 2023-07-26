using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Models.auth.Model;

namespace api.Models
{
    [Table("contacts")]
    public class UsersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        [ForeignKey("UsersAuth")]
        public int userAuthId { get; set; }

        public UsersAuth usersAuth { get; set; }

    }
}