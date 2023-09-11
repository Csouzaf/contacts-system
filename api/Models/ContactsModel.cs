using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Models.auth.Model;

namespace api.Models
{
    [Table("contacts")]
    public class ContactsModel
    {
        [Key]
        [ForeignKey("UserRegisteredModel")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;


        // [ForeignKey("UsersAuth")]
        // [Required]
        // public int userRegisteredId { get; set; }

        // [Required]
        // public UsersAuth? usersAuth { get; set; }

        [Required]
        public UserRegisteredModel? userRegisteredModel { get; set; }

    }
}