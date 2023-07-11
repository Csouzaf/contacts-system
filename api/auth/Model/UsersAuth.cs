using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.auth.Model;

namespace api.Models.auth.Model
{
    [Table("usersAuthenthicate")]
    public class UsersAuth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string telefone { get; set; }

        public AuthUserEmail? authUserEmail { get; set; }

    }
}